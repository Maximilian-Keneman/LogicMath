using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace LogicMath
{
    public partial class MainForm : Form
    {
        private Settings Settings;

        private LogicAbstract Example;

        private bool IsTextActive = false;
        private TextBox ExampleText;
        private KeyboardForm Keyboard;
        private string ClipBoardSave = "";
        private List<List<string>> AlternativeSigns = new List<List<string>>();

        private bool IsDescriptionOpen = false;
        private DescriptionForm Description = new DescriptionForm();

        public MainForm()
        {
            InitializeComponent();
            Settings = new Settings();
            Settings.SettingsChanged += SettingsChanged;
            if (Clipboard.ContainsText())
                ClipBoardSave = Clipboard.GetText();
            ClipBoardTimer.Start();
            ExampleToolTip.SetToolTip(ExampleLabel, "Для ручного ввода дважды нажмите сюда");
            bool IsGood = true;
            if (File.Exists("AlternativeSigns.txt"))
            {
                string[] operations = { "Инверсия:", "Дизъюнкция:", "Конъюнкция:", "Импликация:", "Эквивалентность:", "Неравнозначность:" };
                StreamReader sr = new StreamReader("AlternativeSigns.txt");
                for (int i = 0; i < 6; i++)
                {
                    AlternativeSigns.Add(new List<string>());
                    while (!sr.EndOfStream)
                    {
                        string operation = sr.ReadLine();
                        string[] signs = operation.Split(' ');
                        if (signs[0] == operations[i])
                        {
                            AlternativeSigns[i].AddRange(signs);
                            AlternativeSigns[i].RemoveAt(0);
                            sr.BaseStream.Position = 0;
                            break;
                        }
                    }
                    if (sr.EndOfStream)
                    {
                        IsGood = false;
                        break;
                    }
                }
                if (!IsGood)
                {
                    MessageBox.Show("Файл AlternativeSigns.txt не содержит всех необходимых правильно форматированных строк. Для ввода доступны только обозначения по умолчанию.", "Ошибка в файле", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Файл AlternativeSigns.txt не найден. Для ввода доступны только обозначения по умолчанию.", "Нет файла", MessageBoxButtons.OK, MessageBoxIcon.Information);
                IsGood = false;
            }
        }

        private void Parameters_Changed(object sender, EventArgs e)
        {
            CreateButton.Enabled = VariableNumeric.Value > 0 && (Settings.LogicVector || (SignsNumeric.Value > 0 && OperationsList.CheckedIndices.Count != 0));
        }

        private void DescriptionButton_Click(object sender, EventArgs e)
        {
            if (IsDescriptionOpen)
            {
                Description.Hide();
                IsDescriptionOpen = false;
            }
            else
            {
                Description.Location = new Point(Location.X + Size.Width, Location.Y);
                Description.Show();
                IsDescriptionOpen = true;
            }
        }
        private void ExampleLabel_DoubleClick(object sender, EventArgs e)
        {
            ExampleText = new TextBox()
            {
                Location = ExampleLabel.Location,
                Font = ExampleLabel.Font,
                Text = Settings.LogicVector ? "**" : ""
            };
            ExampleText.Size = new Size(Width - 58, ExampleLabel.Size.Height);
            ExamplePanel.Controls.Add(ExampleText);
            ExampleText.BringToFront();
            CreateButton.Enabled = false;
            VariableNumeric.Enabled = false;
            SignsNumeric.Enabled = false;
            OperationsList.Enabled = false;
            ExampleText.TextChanged += ExampleText_TextChanged;
            ExampleText.KeyPress += ExampleText_KeyPress;
            IsTextActive = true;
            if (!Settings.LogicVector)
            {
                Keyboard = new KeyboardForm(ExampleText);
                Keyboard.DesktopLocation = DesktopLocation + new Size(0, ExampleText.Location.Y + ExampleText.Height + 10);
                Keyboard.Show();
            }
        }
        private void ClipBoardTimer_Tick(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                if (Clipboard.GetText() != ClipBoardSave && Clipboard.GetText() != ExampleLabel.Text)
                {
                    ClipBoardSave = Clipboard.GetText();
                }
                else if (Clipboard.GetText() == ExampleLabel.Text)
                {
                    Clipboard.SetText(ClipBoardSave);
                }
            }
        }
        private void ExampleText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r' && ExampleText.TextLength > 0)
            {
                CreateButton_Click(sender, e);
            }
            if (e.KeyChar == '\u001b')
            {
                ExampleText.Dispose();
                IsTextActive = false;
                CreateButton.Enabled = true;
                VariableNumeric.Enabled = true;
                SignsNumeric.Enabled = true;
                OperationsList.Enabled = true;
                Keyboard?.Close();
                return;
            }
            if (char.IsControl(e.KeyChar))
            {
                return;
            }
            if (Settings.LogicVector)
                ExampleText_LogicVector(ref e);
            else
                ExampleText_LogicFunction(ref e);
        }
        private void ExampleText_LogicFunction(ref KeyPressEventArgs e)
        {
            ExampleText.SelectionStart = ExampleText.TextLength;
            if (ExampleText.TextLength == 0)
            {
                if ((char.IsLetter(e.KeyChar) && e.KeyChar != 'v') || e.KeyChar == '!' || e.KeyChar == '(')
                {
                    e.KeyChar = char.ToUpper(e.KeyChar);
                    return;
                }
            }
            else
            {
                if ((char.IsLetter(e.KeyChar) && e.KeyChar != 'v') || e.KeyChar == '!')
                {
                    if (!char.IsLetter(ExampleText.Text[ExampleText.TextLength - 1]) || ExampleText.Text[ExampleText.TextLength - 1] == 'v')
                    {
                        e.KeyChar = char.ToUpper(e.KeyChar);
                        return;
                    }
                }
                else if (e.KeyChar == '(')
                {
                    if (char.IsLetter(ExampleText.Text[ExampleText.TextLength - 1]) && ExampleText.Text[ExampleText.TextLength - 1] != 'v')
                    {
                        ExampleText.Text += "(+)";
                        ExampleText.SelectionStart = ExampleText.TextLength;
                        e.Handled = true;
                    }
                    else
                    {
                        return;
                    }
                }
                else if (e.KeyChar == ')' || e.KeyChar == '&' || e.KeyChar == '-' || e.KeyChar == '<' || e.KeyChar == 'v')
                {
                    if (char.IsLetter(ExampleText.Text[ExampleText.TextLength - 1]) && ExampleText.Text[ExampleText.TextLength - 1] != 'v')
                    {
                        if (e.KeyChar == '-')
                        {
                            ExampleText.Text += "->";
                            ExampleText.SelectionStart = ExampleText.TextLength;
                            e.Handled = true;
                        }
                        if (e.KeyChar == '<')
                        {
                            ExampleText.Text += "<->";
                            ExampleText.SelectionStart = ExampleText.TextLength;
                            e.Handled = true;
                        }
                        return;
                    }
                    if (ExampleText.TextLength >= 2)
                    {
                        if (ExampleText.Text[ExampleText.TextLength - 1] == ')' && ExampleText.Text[ExampleText.TextLength - 2] != '+')
                        {
                            if (e.KeyChar == '-')
                            {
                                ExampleText.Text += "->";
                                ExampleText.SelectionStart = ExampleText.TextLength;
                                e.Handled = true;
                            }
                            if (e.KeyChar == '<')
                            {
                                ExampleText.Text += "<->";
                                ExampleText.SelectionStart = ExampleText.TextLength;
                                e.Handled = true;
                            }
                            return;
                        }
                    }
                }
            }
            e.Handled = true;
        }
        private void ExampleText_LogicVector(ref KeyPressEventArgs e)
        {
            if ((e.KeyChar == '0' || e.KeyChar == '1') && ExampleText.Text.Length <= Math.Pow(2, 26))
                return;
            e.Handled = true;
        }
        private bool textChange = false;
        private void ExampleText_TextChanged(object sender, EventArgs e)
        {
            if (!textChange)
            {
                textChange = true;
                ExampleError.Clear();
                CreateButton.Enabled = ExampleText.TextLength > 0;
                if (Settings.LogicVector)
                {
                    int select = ExampleText.SelectionStart;
                    ExampleText.Text = ExampleText.Text.Replace("*", "");
                    int current = ExampleText.Text.Length;
                    int count = 2;
                    int i = 1;
                    while (count < current)
                    {
                        count = (int)Math.Pow(2, i);
                        i++;
                    }
                    if (current < count)
                    {
                        ExampleText.Text += new string('*', count - current);
                    }
                    ExampleText.SelectionStart = select;
                }
                textChange = false;
            }
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            NFLabel.Text = "Нормальная форма";
            if (IsTextActive)
            {
                if (Settings.LogicVector)
                {
                    if (!ExampleText.Text.Contains("*"))
                    {
                        Example = new LogicVector(ExampleText.Text);
                        Example.FillProperties();
                        ExampleLabel.Text = Example.ToString();
                        ExampleText.Dispose();
                        IsTextActive = false;
                        CreateButton.Enabled = false;
                        VariableNumeric.Enabled = true;
                    }
                    else
                    {
                        ExampleError.SetError(ExampleText, "Выражение не завершено");
                        return;
                    }
                }
                else
                {
                    int textLength = ExampleText.TextLength;
                    if (textLength > 1 && (
                        (ExampleText.Text[textLength - 1] == ')' && ExampleText.Text[textLength - 2] != '+') ||
                        (char.IsLetter(ExampleText.Text[textLength - 1]) && ExampleText.Text[textLength - 1] != 'v')))
                    {
                        try
                        {
                            if (LogicOperation.CheckBrackets(ExampleText.Text, out string message))
                            {
                                Example = new LogicOperation(LogicOperation.Correct(ExampleText.Text, AlternativeSigns.ToArray()));
                                Example.FillProperties();
                                ExampleLabel.Text = Example.ToString();
                                ExampleText.Dispose();
                                IsTextActive = false;
                                CreateButton.Enabled = false;
                                VariableNumeric.Enabled = true;
                                SignsNumeric.Enabled = true;
                                OperationsList.Enabled = true;
                            }
                            else
                            {
                                ExampleError.SetError(ExampleText, message);
                                return;
                            }
                        }
                        catch (Exception)
                        {
                            ExampleError.SetError(ExampleText, "Введённое выражение не корректно");
                            return;
                        }
                    }
                    else
                    {
                        ExampleError.SetError(ExampleText, "Выражение не завершено");
                        return;
                    }
                }
            }
            else
            {
                int countVar = (int)VariableNumeric.Value;
                if (Settings.LogicVector)
                {
                    Example = new LogicVector(in countVar);
                }
                else
                {
                    int countOper = (int)SignsNumeric.Value;
                    int[] opers = OperationsList.CheckedIndices.Cast<int>().ToArray();
                    Example = new LogicOperation(in countOper, in countVar, opers, opers.Contains(0), Environment.TickCount);
                }
                Example.FillProperties();
                ExampleLabel.Text = Example.ToString();
            }
            Keyboard?.Close();
            if (ExampleLabel.Text != "")
            {
                DNFButton.Enabled = true;
                CNFButton.Enabled = true;
                PDNFButton.Enabled = true;
                PCNFButton.Enabled = true;
                TruthTableButton.Enabled = true;
                SaveButton.Enabled = true;
            }
            else
            {
                DNFButton.Enabled = false;
                CNFButton.Enabled = false;
                PDNFButton.Enabled = false;
                PCNFButton.Enabled = false;
                TruthTableButton.Enabled = false;
                SaveButton.Enabled = false;
                ExampleLabel.Text = "Пример";
            }
        }

        private void TruthTableButton_Click(object sender, EventArgs e)
        {
            TableForm F = new TableForm(Example.TruthTable);
            F.Text = ExampleLabel.Text;
            F.Show();
        }

        private void PDNFButton_Click(object sender, EventArgs e) => NFLabel.Text = Example.PDNF;
        private void DNFButton_Click(object sender, EventArgs e) => NFLabel.Text = Example.DNF;
        private void PCNFButton_Click(object sender, EventArgs e) => NFLabel.Text = Example.PCNF;
        private void CNFButton_Click(object sender, EventArgs e) => NFLabel.Text = Example.CNF;

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (SaveDialog.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(SaveDialog.FileName);
                sw.WriteLine("Выражение: " + ExampleLabel.Text);
                sw.WriteLine();
                sw.WriteLine("Таблица истиности:");
                for (int i = 0; i < Example.Variables.Length; i++)
                    sw.Write(Example.Variables[i] + " ");
                sw.WriteLine();
                sw.WriteLine(Example.TruthTable.ToString());
                sw.WriteLine("ДНФ: " + Example.DNF);
                sw.WriteLine("СДНФ: " + Example.PDNF);
                sw.WriteLine("КНФ: " + Example.CNF);
                sw.WriteLine("СКНФ: " + Example.PCNF);
                sw.Close();
            }
        }

        private void AboutLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AboutForm F = new AboutForm();
            F.Show();
        }
        private void HelpLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Help.ShowHelp(this, "Help.chm");
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            SettingsForm F = new SettingsForm(ref Settings);
            F.Show();
        }
        private void SettingsChanged()
        {
            if (Settings.LogicVector)
            {
                OperationsList.Enabled = false;
                SignsNumeric.Enabled = false;
                DescriptionButton.Enabled = false;
            }
            else
            {
                OperationsList.Enabled = true;
                SignsNumeric.Enabled = true;
                DescriptionButton.Enabled = true;
            }
            Parameters_Changed(this, EventArgs.Empty);
        }
    }
}