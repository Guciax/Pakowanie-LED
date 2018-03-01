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
    public partial class PopupForm : Form
    {
        private readonly string message;
        private readonly int time;

        public PopupForm(string message, int timeSeconds)
        {
            InitializeComponent();
            this.message = message;
            this.time = timeSeconds;
        }

        private void PopupForm_Load(object sender, EventArgs e)
        {
            if (message.Contains("Folia"))
                this.BackColor = Color.Red;
            else
                this.BackColor = Color.Black;
            label1.Text = message;
            timer1.Interval = time * 1000;
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
