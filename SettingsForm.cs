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
    public partial class SettingsForm : Form
    {
        private Settings Settings;
        public SettingsForm(ref Settings settings)
        {
            Settings = settings;
            InitializeComponent();
            radioButton2.Checked = Settings.LogicVector;
            checkBox1.Checked = Settings.Autotyping;
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Settings.LogicVector = radioButton2.Checked;
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Autotyping = checkBox1.Checked;
        }
    }

    public class Settings
    {
        private bool autotyping;
        private bool logicVector;
        public bool Autotyping
        { 
            get => autotyping;
            set
            {
                autotyping = value;
                SettingsChanged?.Invoke();
            }
        }
        public bool LogicVector
        {
            get => logicVector;
            set
            {
                logicVector = value;
                SettingsChanged?.Invoke();
            }
        }

        public event Action SettingsChanged;

        public Settings() : this(true, false) { }
        public Settings(bool autotyping, bool logicVector)
        {
            this.autotyping = autotyping;
            this.logicVector = logicVector;
        }
    }
}
