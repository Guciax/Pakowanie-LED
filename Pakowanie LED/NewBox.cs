using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pakowanie_LED
{
    public partial class NewBox : Form
    {
        private readonly List<string> packingPattern;
        private int modulesPerLayer;
        private readonly DataGridView grid;
        private  int prevWidth;

        public NewBox(List<string> packingPattern, int modulesPerLayer, DataGridView grid, int prevWidth)
        {
            InitializeComponent();
            this.packingPattern = packingPattern;
            this.modulesPerLayer = modulesPerLayer;
            this.grid = grid;
            this.prevWidth = prevWidth;
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
            packingPattern.AddRange(listBoxBox.Items.OfType<string>().ToArray());
            modulesPerLayer = (int)numericUpDown1.Value;
            grid.Rows.Clear();
            grid.Columns.Clear();

            for (int i=0;i<modulesPerLayer;i++)
            {
                grid.Columns.Add("C"+(i + 1).ToString(), (i + 1).ToString());
            }
            
            grid.Rows.Add(listBoxBox.Items.Count);
            Tools.ResizeGrid(grid);

            this.Close();
        }
    }
    
}
