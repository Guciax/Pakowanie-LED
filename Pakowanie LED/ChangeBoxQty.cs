using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pakowanie_LED
{
    public partial class ChangeBoxQty : Form
    {
        private readonly double maxQty;
        private readonly Dictionary<int, PackingLayers> packingPattern;

        public ChangeBoxQty(double maxQty, Dictionary<int, PackingLayers> packingPattern)
        {
            InitializeComponent();
            this.maxQty = maxQty;
            this.packingPattern = packingPattern;
        }

        private void ChangeBoxQty_Load(object sender, EventArgs e)
        {
            label1.Text = "Wprowadź nową ilość (max. "+maxQty+")";
        }

        private void doTheChange()
        {
            Int32 counter = 0;
            if (Int32.TryParse(textBox1.Text, out counter) & counter<maxQty)
            {


                foreach (var item in packingPattern)
                {

                    if (item.Value.LayerName != "Warstwa wyrobów")
                    {
                        if (counter < 1)
                        {
                            item.Value.CompletionTime = new DateTime(1900, 01, 01);
                            item.Value.Completed = false;
                        }
                        else
                        {
                            item.Value.CompletionTime = DateTime.Now;
                            item.Value.Completed = true;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < item.Value.ModuleQrCodes.Count; i++)
                        {
                            if (counter < 1)
                            {
                                item.Value.ModuleQrCodes[i] = "";
                                item.Value.ModuleCompletitionDate[i] = new DateTime(1900, 01, 01);
                                if (i == item.Value.ModuleQrCodes.Count - 1)
                                {
                                    item.Value.CompletionTime = new DateTime(1900, 01, 01);
                                    item.Value.Completed = false;
                                }
                            }
                            else
                            {
                                item.Value.ModuleQrCodes[i] = "QR";
                                item.Value.ModuleCompletitionDate[i] = DateTime.Now; //new DateTime(1900, 01, 01);
                                if (i == item.Value.ModuleQrCodes.Count - 1)
                                {
                                    item.Value.CompletionTime = DateTime.Now;
                                    item.Value.Completed = true;
                                }
                                counter--;
                            }



                        }
                    }



                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Popraw ilość");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            doTheChange();


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                doTheChange();
            }
        }
    }
}
