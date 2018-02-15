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
        List<string> packingPattern = new List<string>();
        int modulesPerLayer = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            NewBox newBoxForm = new NewBox(packingPattern, modulesPerLayer,dataGridView1, 0);
            newBoxForm.Show();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            Tools.ResizeGrid(dataGridView1);
        }
    }
}
