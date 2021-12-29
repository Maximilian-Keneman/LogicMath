using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogicMath
{
    public partial class TableForm : Form
    {
        private const int delta = 114;

        private TruthTable TruthTable;

        private TableLayoutPanel TablePanel;
        public TableForm(TruthTable TruthTable)
        {
            this.TruthTable = TruthTable;
            InitializeComponent();
            SolutionsShowParametrs.Enabled = this.TruthTable.HasOperationsSolutions;
            bw.RunWorkerAsync();
        }

        private void BW_DoWork(object sender, DoWorkEventArgs e)
        {
            TablePanel = Constract(SolutionCheck.Checked, ShortShow.Checked, sender as BackgroundWorker);
        }
        private void BW_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            PaintProgress.Value = e.ProgressPercentage;
        }
        private void BW_RunWorkerComleted(object sender, RunWorkerCompletedEventArgs e)
        {
            TablePanel.CellPaint += new TableLayoutCellPaintEventHandler(TablePanel_CellPaint);
            Controls.Add(TablePanel);
            TablePanel.BringToFront();
            PaintProgress.Visible = false;
            SolutionCheck.Enabled = true;
        }

        private TableLayoutPanel Constract(bool WithSolution, bool IsShort, BackgroundWorker bw)
        {
            TableLayoutPanel TablePanel = new TableLayoutPanel()
            {
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Single,
                Dock = DockStyle.Top
            };
            TablePanel.RowCount = TruthTable.GetRowCount() + 1;
            TablePanel.ColumnCount = TruthTable.GetColumnCount(WithSolution);
            float Fi = (float)((1 + Math.Sqrt(5)) / 2);
            float em = 200f / TablePanel.ColumnCount;
            if (!WithSolution)
                em /= 3;
            else
                em /= 1.5f;
            TablePanel.Size = new Size(TablePanel.Width, (int)(em * Fi * TablePanel.RowCount + 1));
            int m = TablePanel.ColumnCount * TablePanel.RowCount, k = 0;
            for (int i = 0; i < TablePanel.RowCount; i++)
            {
                TablePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / TablePanel.RowCount));
                for (int j = 0; j < TablePanel.ColumnCount; j++)
                {
                    int varL = TruthTable.Variables.Length;
                    float varW = em * Fi * Fi;
                    int count = TablePanel.ColumnCount;
                    ColumnStyle column =
                        WithSolution ? 
                            (j < varL ?
                            new ColumnStyle(SizeType.Absolute, varW) :
                            new ColumnStyle(SizeType.Percent, varW * varL / TablePanel.Width / (count - varL) * 100)) :
                        new ColumnStyle(SizeType.Percent, 100f / count);
                    TablePanel.ColumnStyles.Add(column);
                    Label label = new Label
                    {
                        BackColor = Color.Transparent,
                        Dock = DockStyle.Fill,
                        Font = new Font("Microsoft Sans Serif", em),
                        TextAlign = ContentAlignment.MiddleCenter
                    };
                    if (i == 0)
                    {
                        if (j < TruthTable.Variables.Length)
                            label.Text = TruthTable.Variables[j].ToString();
                        else if (WithSolution)
                            label.Text = IsShort ? TruthTable.OperationsShort[j - TruthTable.Variables.Length]
                                                 : TruthTable.OperationsLong[j - TruthTable.Variables.Length];
                    }
                    else
                    {
                        if (j < TruthTable.Variables.Length)
                            label.Text = TruthTable.InputValues[i - 1, j].ToBinary();
                        else if (j != count - 1 && WithSolution)
                            label.Text = TruthTable.OperationsSolutions[i - 1, j - TruthTable.Variables.Length].ToBinary();
                        else
                            label.Text = TruthTable.OutputValues[i - 1].ToBinary();
                    }
                    TablePanel.Controls.Add(label, j, i);
                    k++;
                    bw.ReportProgress(k * 100 / m);
                }
            }
            return TablePanel;
        }

        private void TablePanel_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            if (e.Column == TablePanel.ColumnCount - 1)
                e.Graphics.FillRectangle(Brushes.LightGreen, e.CellBounds);
            if (SolutionCheck.Checked)
            {
                if (e.Column >= TruthTable.Variables.Length && e.Column != TablePanel.ColumnCount - 1)
                    e.Graphics.FillRectangle(Brushes.LightGray, e.CellBounds);
            }
        }

        private bool IsGrown = false;
        private void SolutionCheck_CheckedChanged(object sender, EventArgs e)
        {
            SolutionCheck.Enabled = false;
            Controls.Remove(TablePanel);
            if (SolutionCheck.Checked && !IsGrown)
            {
                Width *= 2;
                IsGrown = true;
            }
            else if (!SolutionCheck.Checked && IsGrown)
            {
                Width /= 2;
                IsGrown = false;
            }
            PaintProgress.Value = 0;
            PaintProgress.Visible = true;
            bw.RunWorkerAsync();
            ShortShow.Enabled = SolutionCheck.Checked;
        }

        private void TableForm_Resize(object sender, EventArgs e)
        {
            PaintProgress.Size = new Size(Width - delta, 22);
        }
    }
}