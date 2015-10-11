using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rhoa
{
    public class ControlGenerator
    {
        private Rhoa form;
        private List<string> itemParams;
        private int heightAdjust;
        private string url;
        private Dictionary<string, string> generalParams;
        private string itemBase;
        private Settings settings;

        public ControlGenerator(Rhoa Form, string url)
        {
            string itemParamsRaw = Clipboard.GetText();
            itemParams = Regex.Split(itemParamsRaw, "\r\n").ToList();
            this.itemParams = itemParams;

            this.settings = new Settings();
            if (!settings.Valid)
            {
                Application.Exit();
            }
            this.form = Form;
            this.heightAdjust = Mods.ModCount(itemParams) - 1;
            this.url = url;
            this.generalParams = new Dictionary<string, string>();
        }

        public bool GenerateAllControls()
        {
            createSettings();
            itemBase = Mods.GetItemBase(itemParams);
            if (String.IsNullOrEmpty(itemBase))
            {      
                return false;
            }
            generateGeneralControls();
            return true;
        }

        public void addModControls()
        {
            for (int i = itemParams.Count() - 1; i >= 0; i--)
            {
                addControl(itemParams[i]);
            }
            addSearchButton();
        }

        public void addSearchButton()
        {
            Button go = new Button();
            go.Text = "Search";
            go.TextAlign = ContentAlignment.MiddleCenter;
            go.Dock = DockStyle.Bottom;
            go.Click += new EventHandler(this.Search_Click);
            form.AcceptButton = go;
            form.Controls.Add(go);
        }

        private void createTotalResControl()
        {
            const int numOfElements = 3;
            const int numOfTwoStoneRes = 2;

            string[] allElemRes = itemParams.Where(p => p.Contains("to Fire Resistance") || p.Contains("to Cold Resistance")
                        || p.Contains("to Lightning Resistance")).ToArray();

            int totalElemRes = 0;
            if (allElemRes != null)
            {
                for (int i = 0; i < allElemRes.Length; i++)
                {
                    totalElemRes += Int32.Parse(allElemRes[i].Split(' ')[0].Replace("+", "").Replace("%", "").Trim());
                }   
            }
            var toAllElemRes = itemParams.Where(p => p.Contains("to all Elemental Resistances")).ToList();
            if (toAllElemRes != null)
            {
                foreach (var allRes in toAllElemRes)
                {
                    totalElemRes += Int32.Parse(allRes.Split(' ')[0].Replace("+", "").Replace("%", "").Trim()) * numOfElements;
                }
            }

            var twoStoneRes = itemParams.Where(p => p.Contains("to Fire and Lightning Resistances") || p.Contains("to Cold and Lightning Resistances")
                || p.Contains("to Fire and Cold Resistances")).FirstOrDefault();
            if (twoStoneRes != null)
            {
                totalElemRes += Int32.Parse(twoStoneRes.Split(' ')[0].Replace("+", "").Replace("%", "").Trim()) * numOfTwoStoneRes;
            }
            addControl("Total Elem. Res: " + totalElemRes.ToString());

        }

        private void createErrorControl()
        {
            addControl("The selected item type is not yet supported. If it is not on the list of non supported items, please leave a message.");
        }

        private void generateGeneralControls()
        {
            string itemType = ItemTypes.GetItemType(itemBase);
            if (itemType == "No type found")
            {
                createErrorControl();
                return;
            }

            generalParams = new Dictionary<string, string>() { { "type", itemType } , {"league", settings.league}};
            //create controls   
            if (itemBase == "Gem")
            {
                #region Gem controls
                string quality = itemParams.Where(p => p.Contains("Quality:")).FirstOrDefault();
                if (!String.IsNullOrEmpty(quality))
                {
                    heightAdjust++;
                    quality = quality.Split(' ')[1].Replace("+", "").Replace("%", "").Replace("(augmented)", "").Trim();
                }
                else
                {
                    heightAdjust += 2;
                    quality = "0";
                }

                string level = itemParams.Where(p => p.Contains("Level:")).FirstOrDefault();
                if (!string.IsNullOrEmpty(level))
                {
                    level = level.Split(' ')[1].Trim();
                }
                else
                    level = "1";

                addControl("Quality: " + quality);
                addControl("Level: " + level);
                #endregion
            }
            else
                #region Rare general and pseudo mods
                if (!itemParams[0].Contains("Unique"))
                {
                    if (ItemTypes.IsWeapon(itemBase))
                        heightAdjust += 4;
                    if (ItemTypes.IsArmor(itemBase))
                        heightAdjust += 4;
                    if (ItemTypes.IsWeapon(itemBase) || ItemTypes.IsArmor(itemBase))
                        heightAdjust += 2;
                    if (ItemTypes.IsBelt(itemBase) || ItemTypes.IsJewelery(itemBase))
                        heightAdjust++;
                    if (ItemTypes.IsQuiver(itemBase))
                        heightAdjust -= 2;

                    if (ItemTypes.IsWeapon(itemBase))
                    {
                        #region Weapon rare and pseudo mods
                        string[] physDmg = itemParams.Where(p => p.Contains("Physical Damage:")).FirstOrDefault().Split(':')[1].Replace("(augmented)", "").Trim().Split('-');
                        string aps = itemParams.Where(p => p.Contains("Attacks per Second:")).FirstOrDefault().Split(':')[1].Replace("(augmented)", "").Trim();
                        string crit = itemParams.Where(p => p.Contains("Critical Strike Chance:")).FirstOrDefault().Split(':')[1].Replace("%", "").Trim();
                        string elemDmgStr = itemParams.Where(p => p.Contains("Elemental Damage:")).FirstOrDefault();
                        List<string> elemDmg = new List<string>();
                        if (elemDmgStr != null)
                            elemDmg = elemDmgStr.Split(':').ToList()[1].Replace("(augmented)", "").Replace(",", "").Replace("  ", " ").Trim().Split(' ').ToList();
                        //extract dmg, calc pdps edps
                        double pdps = Math.Round((Double.Parse(physDmg[0], CultureInfo.InvariantCulture) + Double.Parse(physDmg[1], CultureInfo.InvariantCulture)) / Double.Parse(aps, CultureInfo.InvariantCulture), 2);
                        double edps = 0;
                        for (int i = 0; i < elemDmg.Count(); i++)
                        {
                            var tempEdps = elemDmg[i].Split('-');
                            double currEdps = Math.Round((Double.Parse(tempEdps[0].Trim(), CultureInfo.InvariantCulture) + Double.Parse(tempEdps[1].Trim(), CultureInfo.InvariantCulture)) / Double.Parse(aps, CultureInfo.InvariantCulture), 2);
                            edps += currEdps;
                        }
                        addControl("pDPS: " + pdps.ToString());
                        addControl("eDPS: " + edps.ToString());
                        addControl("APS: " + aps);
                        addControl("Crit. Strike: " + crit);
                        #endregion
                    }
                    else if (ItemTypes.IsArmor(itemBase))
                    {
                        #region Armor rare and pseudo mods
                        //extract arm es eva
                        string evaTotal = itemParams.Where(p => p.Contains("Evasion Rating:")).FirstOrDefault();
                        if (!String.IsNullOrEmpty(evaTotal))
                            evaTotal = Mods.ExtractModValue(evaTotal);
                        else
                            evaTotal = "0";

                        string shieldTotal = itemParams.Where(p => p.Contains("Energy Shield:")).FirstOrDefault();
                        if (!String.IsNullOrEmpty(shieldTotal))
                            shieldTotal = Mods.ExtractModValue(shieldTotal);
                        else
                            shieldTotal = "0";

                        string armourTotal = itemParams.Where(p => p.Contains("Armour:")).FirstOrDefault();
                        if (!String.IsNullOrEmpty(armourTotal))
                            armourTotal = Mods.ExtractModValue(armourTotal);
                        else
                            armourTotal = "0";

                        createTotalResControl();

                        addControl("Armour: " + armourTotal);
                        addControl("Evasion: " + evaTotal);
                        addControl("Energy Shield: " + shieldTotal);
                        #endregion
                    }
                }
                #endregion

            if (itemParams[0].Contains("Unique") || itemBase == "Gem")
            {
                if (itemBase != "Gem")
                    generalParams["rarity"] = "Unique";
                generalParams["name"] = itemParams[1];
            }

            #region Sockets and Links
            var socketsAndLinks = Mods.GetSocketsAndLinks(itemParams.Where(p => p.Contains("Sockets")).FirstOrDefault());
            if (socketsAndLinks != null)
            {
                if (itemParams[0].Contains("Unique"))
                    heightAdjust += 2;
                addControl("Links: " + socketsAndLinks[1]);
                addControl("Sockets: " + socketsAndLinks[0]);
            }
            #endregion

            if (ItemTypes.IsBelt(itemBase) || ItemTypes.IsJewelery(itemBase))
                createTotalResControl();

            if (itemBase != "Gem")
            {
                itemParams = Mods.FilterItemParams(itemParams);
                addModControls();
            }
            else
            {
                itemParams.Clear();
                addSearchButton();
            }
        }

        private bool addGeneralMod(string modName, string modValue)
        {
            if (modName.Contains("Sockets:"))
            {
                if (modName.Contains("min"))
                    generalParams["sockets_min"] = modValue;
                else
                    generalParams["sockets_max"] = modValue;
                return true;
            }
            if (modName.Contains("Links:"))
            {
                if (modName.Contains("min"))
                    generalParams["links_min"] = modValue;
                else
                    generalParams["links_max"] = modValue;
                return true;
            }
            if (modName.Contains("eDPS:"))
            {
                if (modName.Contains("min"))
                    generalParams["edps_min"] = modValue;
                else
                    generalParams["edps_max"] = modValue;
                return true;
            }
            if (modName.Contains("pdps:"))
            {
                if (modName.Contains("min"))
                    generalParams["pdps_min"] = modValue;
                else
                    generalParams["pdps_max"] = modValue;
                return true;
            }
            if (modName.Contains("APS:"))
            {
                if (modName.Contains("min"))
                    generalParams["aps_min"] = modValue;
                else
                    generalParams["aps_max"] = modValue;
                return true;
            }
            if (modName.Contains("Crit. Strike::"))
            {
                if (modName.Contains("min"))
                    generalParams["crit_min"] = modValue;
                else
                    generalParams["crit_max"] = modValue;
                return true;
            }
            if (modName.Contains("Total Elem. Res"))
            {
                if (modName.Contains("min"))
                    generalParams["totalRes_min"] = modValue;

                else
                    generalParams["totalRes_max"] = modValue;
                return true;
            }
            if (modName.Contains("Level:"))
            {
                if (modName.Contains("min"))
                    generalParams["level_min"] = modValue;

                else
                    generalParams["level_max"] = modValue;
                return true;
            }
            if (modName.Contains("Quality:"))
            {
                if (modName.Contains("min"))
                    generalParams["quality_min"] = modValue;

                else
                    generalParams["quality_max"] = modValue;
                return true;
            }
            if (modName.Contains("Energy Shield:"))
            {
                if (modName.Contains("min"))
                    generalParams["eShield_min"] = modValue;

                else
                    generalParams["eShield_max"] = modValue;
                return true;
            }
            if (modName.Contains("Evasion:"))
            {
                if (modName.Contains("min"))
                    generalParams["evasion_min"] = modValue;

                else
                    generalParams["evasion_max"] = modValue;
                return true;
            }
            if (modName.Contains("Armour:"))
            {
                if (modName.Contains("min"))
                    generalParams["armour_min"] = modValue;

                else
                    generalParams["armour_max"] = modValue;
                return true;
            }
            return false;
        }

        public void addControl(string param)
        {
            int totalWidth = 0;
            string modValue;
            modValue = Mods.ExtractModValue(param);

            //add item mod names
            TextBox modName = new TextBox();
            modName.Text = param;
            modName.ReadOnly = true;
            modName.Dock = DockStyle.Top;
            modName.MaximumSize = new Size((int)(form.Width / 1.1), modName.Height);
            this.form.Controls.Add(modName);
            totalWidth += modName.Width;

            if (param.Contains("(not adjustable)"))
            {
                heightAdjust--;
                return;
            }
            //adjust mod value if adjustable
            Button minMinus = new Button();
            minMinus.Location = new Point(totalWidth, modName.Height * heightAdjust);
            minMinus.Width = 20;
            minMinus.Text = "-";
            minMinus.TextAlign = ContentAlignment.MiddleCenter;
            totalWidth += minMinus.Width;
            minMinus.Click += new EventHandler(minMinus_Click);
            minMinus.Name = "minMinus " + param;

            TextBox minValue = new TextBox();
            minValue.Location = new Point(totalWidth, modName.Height * heightAdjust);
            minValue.Width = 45;
            totalWidth += minValue.Width;
            try
            {
                if (Double.Parse(modValue) < 1)
                    minValue.Text = Math.Round((Double.Parse(modValue, CultureInfo.InvariantCulture) * Double.Parse(settings.InitialMinValuesMultiplied, CultureInfo.InvariantCulture)), 2).ToString();
                else
                    minValue.Text = ((int)(Double.Parse(modValue, CultureInfo.InvariantCulture) * Double.Parse(settings.InitialMinValuesMultiplied, CultureInfo.InvariantCulture))).ToString();
            }
            catch
            {
                minValue.Text = modValue;
            }
            minValue.Name = "minVal " + param;

            Button minPlus = new Button();
            minPlus.Location = new Point(totalWidth, modName.Height * heightAdjust);
            minPlus.Width = 20;
            minPlus.Text = "+";
            minPlus.TextAlign = ContentAlignment.MiddleCenter;
            totalWidth += minPlus.Width;
            minPlus.Click += new EventHandler(minPlus_Click);
            minPlus.Parent = minValue;
            minPlus.Name = "minPlus " + param;

            Button maxMinus = new Button();
            maxMinus.Location = new Point(totalWidth, modName.Height * heightAdjust);
            maxMinus.Width = 20;
            maxMinus.Text = "-";
            maxMinus.TextAlign = ContentAlignment.MiddleCenter;
            totalWidth += maxMinus.Width;
            maxMinus.Click += new EventHandler(maxMinus_Click);
            maxMinus.Name = "maxMinus " + param;

            TextBox maxValue = new TextBox();
            maxValue.Location = new Point(totalWidth, modName.Height * heightAdjust);
            maxValue.Width = 45;
            totalWidth += maxValue.Width;
            try
            {
                if (Double.Parse(modValue) < 1)
                {
                    maxValue.Text = Math.Round((Double.Parse(modValue, CultureInfo.InvariantCulture) * Double.Parse(settings.InitialMaxValuesMultiplied, CultureInfo.InvariantCulture)), 2).ToString();
                }
                else
                    maxValue.Text = ((int)(Double.Parse(modValue, CultureInfo.InvariantCulture) * Double.Parse(settings.InitialMaxValuesMultiplied, CultureInfo.InvariantCulture))).ToString();
                if (maxValue.Text == "0")
                    maxValue.Text = "";
            }
            catch
            {
                maxValue.Text = modValue;
            }
            maxValue.Name = "maxVal " + param; ;

            Button maxPlus = new Button();
            maxPlus.Location = new Point(totalWidth, modName.Height * heightAdjust);
            maxPlus.Width = 20;
            maxPlus.Text = "+";
            maxPlus.TextAlign = ContentAlignment.MiddleCenter;
            totalWidth += maxPlus.Width;
            maxPlus.Click += new EventHandler(this.maxPlus_Click);
            maxPlus.Name = "maxPlus " + param;

            this.form.Controls.Add(minMinus);
            this.form.Controls.Add(minValue);
            this.form.Controls.Add(minPlus);

            this.form.Controls.Add(maxMinus);
            this.form.Controls.Add(maxValue);
            this.form.Controls.Add(maxPlus);

            //select mod to compare by
            CheckBox checkbox = new CheckBox();
            checkbox.Location = new Point(totalWidth + 2, modName.Height * heightAdjust);
            checkbox.Width = 20;
            totalWidth += checkbox.Width;
            checkbox.Name = "cBox " + param;
            this.form.Controls.Add(checkbox);
            if (settings.InitiallyAllSelected != "0")
                checkbox.Checked = true;
            heightAdjust--;
        }

        private void createSettings()
        {
            ToolBar settingsToolbar = new ToolBar();
            ToolBarButton settingsButton = new ToolBarButton();
            settingsButton.Text = "Settings";
            settingsToolbar.ButtonClick += new ToolBarButtonClickEventHandler(settings_Click);
            settingsToolbar.Buttons.Add(settingsButton);
            ToolBarButton aboutButton = new ToolBarButton();
            aboutButton.Text = "About";
            settingsToolbar.Buttons.Add(aboutButton);
            form.Controls.Add(settingsToolbar);
        }

        #region Event Handlers
        private void settings_Click(object sender, ToolBarButtonClickEventArgs e)
        {
            if(e.Button.Text == "About")
            {
                MessageBox.Show("Made by Nomad, for all the exiles.","I am Greust, I hunt boar.");
                return;
            }
            SettingsForm settingsForm = new SettingsForm(settings);
            settingsForm.ShowDialog();
        }
        private void minMinus_Click(object sender, System.EventArgs e)
        {
            string buttonName = (sender as Button).Name;
            string btnName = buttonName;
            buttonName = buttonName.Replace("Minus", "Val");
            var box = this.form.Controls.Find(buttonName, true).FirstOrDefault() as TextBox;
            if (string.IsNullOrEmpty(box.Text))
                box.Text = "1";
            bool isIncrement = IsIncrementOne(btnName, box.Text);
            //if decimal
            if (box.Text.Contains(".") && !isIncrement)
            {
                if(settings.additionOrMultiplication == "addition")
                    box.Text = (Double.Parse(box.Text, CultureInfo.InvariantCulture) - Double.Parse(settings.AddAndSubtractBy, CultureInfo.InvariantCulture)).ToString();
                else
                    box.Text = (Double.Parse(box.Text, CultureInfo.InvariantCulture) * Double.Parse(settings.MultiplyReductionBy, CultureInfo.InvariantCulture)).ToString();
            }
            else
            {
                if (settings.additionOrMultiplication == "addition" || isIncrement)
                    box.Text = ((int)(Double.Parse(box.Text, CultureInfo.InvariantCulture) - Double.Parse(settings.AddAndSubtractBy, CultureInfo.InvariantCulture))).ToString();
                else
                    box.Text = ((int)(Double.Parse(box.Text, CultureInfo.InvariantCulture) * Double.Parse(settings.MultiplyReductionBy, CultureInfo.InvariantCulture))).ToString();
            }
        }

        private void minPlus_Click(object sender, EventArgs e)
        {
            string buttonName = (sender as Button).Name;
            string btnName = buttonName;
            buttonName = buttonName.Replace("Plus", "Val");
            var box = this.form.Controls.Find(buttonName, true).FirstOrDefault() as TextBox;
            if (string.IsNullOrEmpty(box.Text))
                box.Text = "1";
            bool isIncrement = IsIncrementOne(btnName, box.Text);
            //if decimal
            if (box.Text.Contains(".") && !isIncrement)
            {
                if (settings.additionOrMultiplication == "addition")
                    box.Text = (Double.Parse(box.Text, CultureInfo.InvariantCulture) + Double.Parse(settings.AddAndSubtractBy, CultureInfo.InvariantCulture)).ToString();
                else
                    box.Text = (Double.Parse(box.Text, CultureInfo.InvariantCulture) * Double.Parse(settings.MultiplyIncreaseBy, CultureInfo.InvariantCulture)).ToString();
            }
            else
            {
                if (settings.additionOrMultiplication == "addition" || isIncrement)
                    box.Text = ((int)(Double.Parse(box.Text, CultureInfo.InvariantCulture) + Double.Parse(settings.AddAndSubtractBy, CultureInfo.InvariantCulture))).ToString();
                else
                    box.Text = ((int)(Double.Parse(box.Text, CultureInfo.InvariantCulture) * Double.Parse(settings.MultiplyIncreaseBy, CultureInfo.InvariantCulture))).ToString();
            }
        }

        private void maxMinus_Click(object sender, EventArgs e)
        {
            var buttonName = (sender as Button).Name;
            string btnName = buttonName;
            buttonName = buttonName.Replace("Minus", "Val");
            var box = this.form.Controls.Find(buttonName, true).FirstOrDefault() as TextBox;
            if (string.IsNullOrEmpty(box.Text))
                box.Text = "1";
            bool isIncrement = IsIncrementOne(btnName, box.Text);
            //if decimal
            if (box.Text.Contains(".") && !isIncrement)
            {
                if (settings.additionOrMultiplication == "addition")
                    box.Text = (Double.Parse(box.Text, CultureInfo.InvariantCulture) - Double.Parse(settings.AddAndSubtractBy, CultureInfo.InvariantCulture)).ToString();
                else
                    box.Text = (Double.Parse(box.Text, CultureInfo.InvariantCulture) * Double.Parse(settings.MultiplyReductionBy, CultureInfo.InvariantCulture)).ToString();
            }
            else
            {
                if (settings.additionOrMultiplication == "addition" || isIncrement)
                    box.Text = ((int)(Double.Parse(box.Text, CultureInfo.InvariantCulture) - Double.Parse(settings.AddAndSubtractBy, CultureInfo.InvariantCulture))).ToString();
                else
                    box.Text = ((int)(Double.Parse(box.Text, CultureInfo.InvariantCulture) * Double.Parse(settings.MultiplyReductionBy, CultureInfo.InvariantCulture))).ToString();
            }
        }

        private void maxPlus_Click(object sender, EventArgs e)
        {
            string buttonName = (sender as Button).Name;
            string btnName = buttonName;
            buttonName = buttonName.Replace("Plus", "Val");
            var box = this.form.Controls.Find(buttonName, true).FirstOrDefault() as TextBox;
            if (string.IsNullOrEmpty(box.Text))
                box.Text = "1";
            bool isIncrement = IsIncrementOne(btnName, box.Text);
            //if decimal
            if (box.Text.Contains(".") && !isIncrement)
            {
                if (settings.additionOrMultiplication == "addition")
                    box.Text = (Double.Parse(box.Text, CultureInfo.InvariantCulture) + Double.Parse(settings.AddAndSubtractBy, CultureInfo.InvariantCulture)).ToString();
                else
                    box.Text = (Double.Parse(box.Text, CultureInfo.InvariantCulture) * Double.Parse(settings.MultiplyIncreaseBy, CultureInfo.InvariantCulture)).ToString();
            }
            else
            {
                if (settings.additionOrMultiplication == "addition" || isIncrement)
                    box.Text = ((int)(Double.Parse(box.Text, CultureInfo.InvariantCulture) + Double.Parse(settings.AddAndSubtractBy, CultureInfo.InvariantCulture))).ToString();
                else
                    box.Text = ((int)(Double.Parse(box.Text, CultureInfo.InvariantCulture) * Double.Parse(settings.MultiplyIncreaseBy, CultureInfo.InvariantCulture))).ToString();
            }
        }

        private void Search_Click(object sender, System.EventArgs e)
        {
            var btn = sender as Button;
            btn.Text = "Sending request to xyz, needs 5 to 20 sec (depending on # of results)";
            WebBrowser browser = new WebBrowser();
            browser.AllowNavigation = true;
            browser.Navigated += new WebBrowserNavigatedEventHandler(xyzLoaded);
            browser.ScriptErrorsSuppressed = true;
            browser.Navigate(url);
        }

        private void xyzLoaded(object sender, WebBrowserNavigatedEventArgs e)
        {
            WebBrowser browser = sender as WebBrowser;
            browser.AllowNavigation = true;
            string response = browser.DocumentText;
            browser.Navigated -= new WebBrowserNavigatedEventHandler(xyzLoaded);
            List<string> queryValues = new List<string>();
            foreach (var control in this.form.Controls)
            {
                //value box
                try
                {
                    double value;
                    var box = control as TextBox;
                    string boxName;
                    if (box.Name.Contains("minVal"))
                        boxName = box.Name.Replace("minVal", "cBox");
                    else
                        boxName = box.Name.Replace("maxVal", "cBox");

                    CheckBox checkbox = this.form.Controls.Find(boxName, true).FirstOrDefault() as CheckBox;
                    if (!checkbox.Checked)
                    {
                        itemParams.RemoveAll(p => p.Contains(checkbox.Name.Replace("cBox ", "")));
                        continue;
                    }
                    bool isNum = Double.TryParse(box.Text, out value);
                    if (isNum || box.Text == "")
                    {
                        if (!addGeneralMod(box.Name, box.Text))
                            queryValues.Add(box.Text);
                    }
                }
                catch
                {
                };
            }
            
            queryValues.Reverse();
            string query = QueryGenerator.GenerateQuery(itemParams, queryValues, generalParams);
            //send the post
            ASCIIEncoding encoding = new ASCIIEncoding();
            browser.ScriptErrorsSuppressed = true;
            browser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(searchFinished);

            browser.Navigate(url + "/search", "", encoding.GetBytes(query), "Content-Type: application/x-www-form-urlencoded");
        }

        private void searchFinished(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser browser = sender as WebBrowser;
            string url = browser.Url.ToString();
            browser.DocumentCompleted -= new WebBrowserDocumentCompletedEventHandler(searchFinished);
            Process.Start(url);
            Application.Exit();
        }
        #endregion

        private bool IsIncrementOne(string boxName, string boxValue)
        {
            if (string.IsNullOrEmpty(boxValue))
                return true;
            var value = Double.Parse(boxValue, CultureInfo.InvariantCulture);
            if (boxName.Contains("Minus"))
            {
                var res = (int)(value * Double.Parse(settings.MultiplyReductionBy, CultureInfo.InvariantCulture));
                if (res == value || res == 0)
                    return true;
            }

            if (boxName.Contains("Plus"))
            {
                var res = ((int)(value * Double.Parse(settings.MultiplyIncreaseBy, CultureInfo.InvariantCulture)));
                if (res == value || res == 0)
                    return true;
            }

            if (!boxName.Contains("Sockets:") && !boxName.Contains("Quality:") && !boxName.Contains("Level:") && !boxName.Contains("Links:"))
                return false;
            return true;
        }
    }
}
