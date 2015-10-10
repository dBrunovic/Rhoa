using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Rhoa
{
    public class Settings
    {
        public string league;
        public string additionOrMultiplication;
        public string AddAndSubtractBy;
        public string InitialMinValuesMultiplied;
        public string InitialMaxValuesMultiplied;
        public string MultiplyReductionBy;
        public string MultiplyIncreaseBy;
        public string InitiallyAllSelected;
        public bool Valid;

        private string path;
        private string errorMsg = "Settings invalid, check config file, Check value of: ";
        public Settings()
        {
            string settingsFileName = "ConfigRhoa.cfg";
            
            path = Path.Combine(Application.StartupPath, settingsFileName);
            Valid = true;        
            try
            {
                var values = File.ReadAllLines(path);
                setSettings(values);
            }
            catch
            {
                MessageBox.Show("No config file found. Add a valid ConfigRhoa.cfg file to the folder containing the exe. Generating default config file...");
                saveSettings(true);
            }
        }

        private void setSettings(string[] values)
        {
            try
            {
                league = values[0].Split(':')[1].Split('/')[0].Trim();
                additionOrMultiplication = values[1].Split(':')[1].Split('/')[0].Trim();
                AddAndSubtractBy = values[2].Split(':')[1].Split('/')[0].Trim();
                InitialMinValuesMultiplied = values[3].Split(':')[1].Split('/')[0].Trim();
                InitialMaxValuesMultiplied = values[4].Split(':')[1].Split('/')[0].Trim();
                MultiplyReductionBy = values[5].Split(':')[1].Split('/')[0].Trim();
                MultiplyIncreaseBy = values[6].Split(':')[1].Split('/')[0].Trim();
                InitiallyAllSelected = values[7].Split(':')[1].Split('/')[0].Trim();
                ValidateSettingValues();
            }
            catch
            {
                MessageBox.Show("Invalid format of config file, please follow the default format and change just the values to valid values");
                Valid = false;
            }
        }

        public void saveSettings(bool defaultSettings = false)
        {
            if (defaultSettings)
            {
                league = "Standard";
                additionOrMultiplication = "multiplication";
                AddAndSubtractBy = "1";
                InitialMinValuesMultiplied = "1";
                InitialMaxValuesMultiplied = "0";
                MultiplyReductionBy = "0.05";
                MultiplyIncreaseBy = "1.05";
                InitiallyAllSelected = "0";
            }
            try
            {
                File.WriteAllText(path,"League:"+league+" //league name instead of Standard"+Environment.NewLine+
                "UsesAdditionOrMultiplication:"+additionOrMultiplication+" //use addition\" or \"multiplication\" for min max value + and -"+Environment.NewLine+
                "AddAndSubtractBy:"+AddAndSubtractBy+" //the number by which the min and max values are increased or decreased"+Environment.NewLine+
                "InitialMinValuesMultiplied:"+InitialMinValuesMultiplied+" //the number by which the initial min values are multiplied by"+Environment.NewLine+
                "InitialMaxValuesMultiplied:"+InitialMaxValuesMultiplied+" //same as min"+Environment.NewLine+
                "MultiplyReductionBy:"+MultiplyReductionBy+" //the number by which the min values are multiplied by"+Environment.NewLine+
                "MultiplyIncreaseBy:"+MultiplyIncreaseBy+" //the number by which the max values are multiplied by"+Environment.NewLine+
                "InitiallyAllSelected:"+InitiallyAllSelected+" //0 for all mods to be deselected, 1 for all mods to be selected");
            }
            catch
            {
                MessageBox.Show("Could not create a config file in the exe folder. Probably don't have permission.");
                Valid = false;
                return;
            }
        }

        public void ValidateSettingValues()
        {
            double temp;
            bool valid;

            if (additionOrMultiplication != "addition" && additionOrMultiplication != "multiplication")
            {
                this.Valid   = false;
                errorMsg += "UsesAdditionOrMultiplication ";
            }

            valid = double.TryParse(AddAndSubtractBy, out temp);
            if (!valid)
            {
                this.Valid = false;
                errorMsg += "AddAndSubtractBy ";
            }

            valid = double.TryParse(InitialMaxValuesMultiplied, out temp);
            if (!valid)
            {
                this.Valid = false;
                errorMsg += "InitialMaxValuesMultiplied ";
            }

            valid = double.TryParse(InitialMinValuesMultiplied, out temp);
            if (!valid)
            {
                this.Valid = false;
                errorMsg += "InitialMinValuesMultiplied ";
            }

            valid = double.TryParse(MultiplyReductionBy, out temp);
            if (!valid)
            {
                this.Valid = false;
                errorMsg += "MultiplyReductionBy ";
            }

            valid = double.TryParse(MultiplyIncreaseBy, out temp);
            if (!valid)
            {
                this.Valid = false;
                errorMsg += "MultiplyIncreaseBy ";
            }
            if(InitiallyAllSelected != "0" && InitiallyAllSelected != "1")
            {
                this.Valid = false;
                errorMsg += "InitiallyAllSelected ";
            }
            if (!this.Valid)
                MessageBox.Show(errorMsg);
        }
    }
}
