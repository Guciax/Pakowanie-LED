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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Dictionary<int, PackingLayers> packingPattern = new Dictionary<int, PackingLayers>();

        List<PackingLayers> currentBox = new List<PackingLayers>();

        private void button1_Click(object sender, EventArgs e)
        {
            NewBox newBoxForm = new NewBox(packingPattern, dataGridView1);
            newBoxForm.Show();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            Tools.ResizeGrid(dataGridView1);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                Tools.NewQrScanned(packingPattern, textBox1.Text);
                Tools.UpdateLayerColors(dataGridView1, packingPattern);
            }
        }
    }
}
