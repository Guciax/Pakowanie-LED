using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pakowanie_LED
{
    public partial class NewBox : Form
    {
        private readonly Dictionary<int, PackingLayers> packingPattern;
        private readonly DataGridView grid;
        private readonly PictureBox pctBox;
        private  double totalLed;

        public NewBox(Dictionary<int,PackingLayers> packingPattern, DataGridView grid, PictureBox pctBox, double totalLed)
        {
            InitializeComponent();
            this.packingPattern = packingPattern;
            this.grid = grid;
            this.pctBox = pctBox;
            this.totalLed = totalLed;
        }

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBoxPart.Text != "")
                listBoxPattern.Items.Insert(0,comboBoxPart.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBoxPattern.Items.RemoveAt(listBoxPattern.SelectedIndex);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBoxPattern.Items.Count > 0)
            {
                for (int i=listBoxPattern.Items.Count-1;i>-1;i--)
                {
                    listBoxBox.Items.Insert(0, listBoxPattern.Items[i]);
                }
                
                UpdateQuantity();
            }
        }
        int modulesCount = 0;
        private void UpdateQuantity()
        {
            int foilCount = 0;
            int spacerCount = 0;
            modulesCount = 0;
            foreach (var item in listBoxBox.Items)
            {
                if (item.ToString() == "Folia") foilCount++;
                if (item.ToString() == "Przekładka") spacerCount++;
                if (item.ToString() == "Warstwa wyrobów") modulesCount++;
            }


            labelSummary.Text = "Folia: " + foilCount + " szt.";
            labelSummary.Text += Environment.NewLine+"Przekładki: " + spacerCount + " szt.";
            labelSummary.Text += Environment.NewLine + "Wyroby: " + modulesCount * numericUpDown1.Value + " szt. (" + modulesCount + "warstw).";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBoxBox.Items.RemoveAt(listBoxBox.SelectedIndex);
            UpdateQuantity();
        }

        private void listBoxBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            //string i = listBoxBox.Items[e.Index].ToString();
            //if (listBoxBox.Items[e.Index].ToString() == "Folia")
            //{
            //    e.DrawBackground();
            //    e.Graphics.DrawString(listBoxBox.Items[e.Index].ToString(), listBoxBox.Font, Brushes.Pink, e.Bounds);
        }
       
        
        
        private void button5_Click(object sender, EventArgs e)
        {
            packingPattern.Clear();
            for (int i = listBoxBox.Items.Count - 1; i > -1; i--) 
            {
                List<string> modules = new List<string>();
                List<DateTime> dates = new List<DateTime>();

                for (int m = 0; m < numericUpDown1.Value; m++) 
                {
                    modules.Add("");
                    dates.Add(new DateTime(1900, 01, 01));
                }
                PackingLayers newLayer = new PackingLayers(listBoxBox.Items[i].ToString(), false, new DateTime(1900, 01, 01), (int)numericUpDown1.Value, modules, dates);
                packingPattern.Add(i,newLayer);
                
            }

            this.Visible = false;
            Tools.UpdateFoilAndSpacerCompletition(packingPattern);
            pctBox.Image = Tools.DrawBitmap(packingPattern, pctBox);
            this.Close();
        }

        private void listBoxBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public static void AddOrUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }

        private void removeSettingsKey(string key)
        {

            var m_Configuration = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            m_Configuration.AppSettings.Settings.Remove(key);
            m_Configuration.Save(ConfigurationSaveMode.Modified);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string layers = string.Join(";", listBoxBox.Items.OfType<string>().ToArray());
            string promptValue = Prompt.ShowDialog("Podaj nazwę", "Nazwa lub 12NC kartonu");
            AddOrUpdateAppSettings(promptValue, layers);
        }

        private void NewBox_Load(object sender, EventArgs e)
        {
            foreach (var key in ConfigurationManager.AppSettings.AllKeys)
            {
                comboBox1.Items.Add(key);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text.Trim().Length > 0)
            {
                try
                {
                    string[] value = System.Configuration.ConfigurationManager.AppSettings[comboBox1.Text].Split(';');
                    listBoxBox.Items.AddRange(value);
                }
                catch (Exception ex)
                {  }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            removeSettingsKey(comboBox1.Text);
        }
    }
}
