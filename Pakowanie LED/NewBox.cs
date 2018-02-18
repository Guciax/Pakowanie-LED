using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public NewBox(Dictionary<int,PackingLayers> packingPattern, DataGridView grid)
        {
            InitializeComponent();
            this.packingPattern = packingPattern;
            this.grid = grid;
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
            listBoxBox.Items.AddRange(listBoxPattern.Items);
            UpdateQuantity();
        }

        private void UpdateQuantity()
        {
            int foilCount = 0;
            int spacerCount = 0;
            int modulesCount = 0;
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
            for (int i = listBoxBox.Items.Count - 1; i > -1; i--) 
            {
                List<string> modules = new List<string>();
                for(int m=0;m<numericUpDown1.Value;m++)
                {
                        modules.Add("");
                }

                PackingLayers newLayer = new PackingLayers(listBoxBox.Items[i].ToString(), false, new DateTime(1900, 01, 01), (int)numericUpDown1.Value, modules);
                packingPattern.Add(i,newLayer);
            }

            grid.Rows.Clear();
            grid.Columns.Clear();

            for (int i = 0; i < numericUpDown1.Value; i++) 
            {
                grid.Columns.Add("C"+(i + 1).ToString(), (i + 1).ToString());
            }

            for (int r = 0; r < listBoxBox.Items.Count; r++)
            {
                grid.Rows.Add(listBoxBox.Items.Count);

            }
            Tools.ResizeGrid(grid);
            this.Visible = false;
            Tools.NewQrScanned(packingPattern, "");

            this.Close();
        }

        private void listBoxBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
