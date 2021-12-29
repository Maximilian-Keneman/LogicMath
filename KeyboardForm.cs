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
    public partial class KeyboardForm : Form
    {
        private TextBox ExampleText;
        private bool VorS = true;

        public KeyboardForm(TextBox ExampleText)
        {
            InitializeComponent();
            this.ExampleText = ExampleText;
        }

        private void PressButton(object sender, EventArgs e)
        {
            ExampleText.Text += (sender as Control).Text;
        }

        private void Backspace_Click(object sender, EventArgs e)
        {
            ExampleText.Text = ExampleText.Text.Remove(ExampleText.TextLength - 1);
        }
    }
}
