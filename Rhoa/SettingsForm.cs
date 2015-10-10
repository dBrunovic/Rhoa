using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rhoa
{
    public partial class SettingsForm : Form
    {
        private Settings settings;
        public SettingsForm(Settings settings)
        {
            this.settings = settings;
            this.Focus();
            this.TopMost = true;
            Load += new EventHandler(Window_Load);
            InitializeComponent();
        }

        void Window_Load(object sender, EventArgs e)
        {
            //initialize values
            (Controls.Find("League", true).FirstOrDefault() as TextBox).Text = settings.league;

            if (settings.additionOrMultiplication == "addition")
                (Controls.Find("UseAdditionRadioButton", true).FirstOrDefault() as RadioButton).Checked = true;
            else
                (Controls.Find("UseMultRadioButton", true).FirstOrDefault() as RadioButton).Checked = true;

            CheckBox initSelected = Controls.Find("InitCheckedAllSelected", true).FirstOrDefault() as CheckBox;
            if (settings.InitiallyAllSelected == "0")
                initSelected.Checked = false;
            else
                initSelected.Checked = true;

            (Controls.Find("InitMinValTextBox", true).FirstOrDefault() as TextBox).Text = settings.InitialMinValuesMultiplied;
            (Controls.Find("InitMaxValTextBox", true).FirstOrDefault() as TextBox).Text = settings.InitialMaxValuesMultiplied;
            (Controls.Find("MultiplyReductionTextBox", true).FirstOrDefault() as TextBox).Text = settings.MultiplyReductionBy;
            (Controls.Find("MultiplyIncreaseTextBox", true).FirstOrDefault() as TextBox).Text = settings.MultiplyIncreaseBy;
            (Controls.Find("AddAndSubtractByTextbox", true).FirstOrDefault() as TextBox).Text = settings.AddAndSubtractBy;          
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            //extract values
            settings.league = (Controls.Find("League", true).FirstOrDefault() as TextBox).Text;

            if((Controls.Find("UseAdditionRadioButton", true).FirstOrDefault() as RadioButton).Checked)
                settings.additionOrMultiplication = "addition";
            else
                settings.additionOrMultiplication = "multiplication";

            CheckBox initSelected = Controls.Find("InitCheckedAllSelected", true).FirstOrDefault() as CheckBox;
            if (initSelected.Checked)
                settings.InitiallyAllSelected = "1";
            else
                settings.InitiallyAllSelected = "0";

            settings.InitialMinValuesMultiplied = (Controls.Find("InitMinValTextBox", true).FirstOrDefault() as TextBox).Text;
            settings.InitialMaxValuesMultiplied = (Controls.Find("InitMaxValTextBox", true).FirstOrDefault() as TextBox).Text;
            settings.MultiplyReductionBy = (Controls.Find("MultiplyReductionTextBox", true).FirstOrDefault() as TextBox).Text;
            settings.MultiplyIncreaseBy = (Controls.Find("MultiplyIncreaseTextBox", true).FirstOrDefault() as TextBox).Text;
            settings.AddAndSubtractBy = (Controls.Find("AddAndSubtractByTextbox", true).FirstOrDefault() as TextBox).Text;
            //validate values
            settings.ValidateSettingValues();
            if (!settings.Valid)
                return;
            //save values
            settings.saveSettings();
            this.Close();
        }
    }
}
