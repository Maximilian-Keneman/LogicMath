namespace LogicMath
{
    partial class TableForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TableForm));
            this.bw = new System.ComponentModel.BackgroundWorker();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.PaintProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.SolutionsShowParametrs = new System.Windows.Forms.ToolStripDropDownButton();
            this.SolutionCheck = new System.Windows.Forms.ToolStripMenuItem();
            this.ShortShow = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bw
            // 
            this.bw.WorkerReportsProgress = true;
            this.bw.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BW_DoWork);
            this.bw.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BW_ProgressChanged);
            this.bw.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BW_RunWorkerComleted);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PaintProgress,
            this.SolutionsShowParametrs});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(318, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // PaintProgress
            // 
            this.PaintProgress.Name = "PaintProgress";
            this.PaintProgress.Size = new System.Drawing.Size(220, 22);
            this.PaintProgress.Visible = false;
            // 
            // SolutionsShowParametrs
            // 
            this.SolutionsShowParametrs.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.SolutionsShowParametrs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.SolutionsShowParametrs.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SolutionCheck,
            this.ShortShow});
            this.SolutionsShowParametrs.Image = ((System.Drawing.Image)(resources.GetObject("SolutionsShowParametrs.Image")));
            this.SolutionsShowParametrs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SolutionsShowParametrs.Name = "SolutionsShowParametrs";
            this.SolutionsShowParametrs.Size = new System.Drawing.Size(84, 19);
            this.SolutionsShowParametrs.Text = "Параметры";
            // 
            // SolutionCheck
            // 
            this.SolutionCheck.CheckOnClick = true;
            this.SolutionCheck.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.SolutionCheck.Name = "SolutionCheck";
            this.SolutionCheck.Size = new System.Drawing.Size(210, 22);
            this.SolutionCheck.Text = "Показать решение";
            this.SolutionCheck.CheckedChanged += new System.EventHandler(this.SolutionCheck_CheckedChanged);
            // 
            // ShortShow
            // 
            this.ShortShow.CheckOnClick = true;
            this.ShortShow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ShortShow.Enabled = false;
            this.ShortShow.Name = "ShortShow";
            this.ShortShow.Size = new System.Drawing.Size(210, 22);
            this.ShortShow.Text = "Краткая запись действий";
            this.ShortShow.CheckedChanged += new System.EventHandler(this.SolutionCheck_CheckedChanged);
            // 
            // TableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(318, 363);
            this.Controls.Add(this.toolStrip1);
            this.Name = "TableForm";
            this.ShowIcon = false;
            this.Text = "Table";
            this.Resize += new System.EventHandler(this.TableForm_Resize);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.ComponentModel.BackgroundWorker bw;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripProgressBar PaintProgress;
        private System.Windows.Forms.ToolStripDropDownButton SolutionsShowParametrs;
        private System.Windows.Forms.ToolStripMenuItem SolutionCheck;
        private System.Windows.Forms.ToolStripMenuItem ShortShow;
    }
}