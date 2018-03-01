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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }
        Dictionary<int, PackingLayers> packingPattern = new Dictionary<int, PackingLayers>();
        List<PackingLayers> currentBox = new List<PackingLayers>();
        double totalLeds = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            NewBox newBoxForm = new NewBox(packingPattern, dataGridView1,pictureBox1, totalLeds);
            newBoxForm.Show();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            pictureBox1.Image = Tools.DrawBitmap(packingPattern, pictureBox1);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                Tools.AddnewQr(packingPattern, textBox1.Text, pictureBox1);
                pictureBox1.Image = Tools.DrawBitmap(packingPattern, pictureBox1);
                label2.Text = Tools.CountLedPanels(packingPattern).ToString() + @"/"+ countPanles();
                if (packingPattern.Count > 1)
                {
                    labelEffciency.Text = Math.Round(Tools.CountModulesPerHour(packingPattern), 0).ToString() + @"/h";
                }
                textBox1.Text = "";
            }
        }

        private int countPanles()
        {
            //return packingPattern.Select(s => s.Value.ModuleQrCodes.Count).Sum();
            return packingPattern.Select(s => s.Value).Where(l => l.LayerName == "Warstwa wyrobów").Select(m => m.ModuleQrCodes.Count()).Sum();
        }

        class Rectangle
        {
            public float width;
            public float heigth;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            //if (packingPattern.Count > 0)
            //{
            //    int numberOfFoilLayers = packingPattern.Select(s => s.Value).Where(w => w.LayerName.Contains("Foli")).Count();
            //    int numberOfSpacersLayer = packingPattern.Select(s => s.Value).Where(w => w.LayerName.Contains("Przek")).Count();
            //    int numberOfLedLayers = packingPattern.Select(s => s.Value).Where(w => w.LayerName.Contains("wyrob")).Count();
            //    int numberOfAllLayers = packingPattern.Count;

            //    int canvasWidth = panel2.Width;
            //    int canvasHeigth = panel2.Height;

            //    int marginLeft = 2;
            //    int marginRight = 2;
            //    int marginTop = 2;
            //    int marginBottom = 2;

            //    Rectangle foilRec = new Rectangle();
            //    Rectangle spacerRec = new Rectangle();
            //    Rectangle ledRec = new Rectangle();

            //    int numberOfLedPerLayer = 0;
            //    foreach (var item in packingPattern)
            //    {
            //        if (item.Value.LayerName == "Warstwa wyrobów")
            //        {
            //            numberOfLedPerLayer = item.Value.ModuleQrCodes.Count;
            //            break;
            //        }
            //    }

            //    foilRec.width = canvasWidth - marginLeft - marginRight;
            //    foilRec.heigth = (canvasHeigth - marginTop - marginBottom) / numberOfAllLayers * (float)0.8;

            //    spacerRec.width = canvasWidth - marginLeft - marginRight;
            //    spacerRec.heigth = (canvasHeigth - marginTop - marginBottom) / numberOfAllLayers * (float)0.8;

            //    ledRec.width = (canvasWidth - marginLeft - marginRight) / numberOfLedPerLayer;
            //    ledRec.heigth = (canvasHeigth - marginTop - marginBottom - foilRec.heigth*numberOfFoilLayers - spacerRec.heigth*numberOfSpacersLayer) / numberOfLedLayers;

            //    Pen borderPen = new Pen(Color.Black, 1);
            //    System.Drawing.SolidBrush solidYellowBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Yellow);
            //    System.Drawing.SolidBrush solidBlackBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            //    System.Drawing.SolidBrush solidRedBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
            //    float y = marginTop;
            //    for (int i = 0; i < packingPattern.Count; i++)
            //    {
                    
                    


            //        switch (packingPattern[i].LayerName)
            //        {
            //            case "Warstwa wyrobów":
            //                {

                                
            //                    float w = ledRec.width;
            //                    float h = ledRec.heigth;

            //                    for (int led = 0; led < packingPattern[i].ModuleQrCodes.Count; led++)
            //                    {
            //                        float x = marginLeft + led * ledRec.width;
            //                        if (packingPattern[i].ModuleQrCodes[led] != "")
            //                        {
            //                            e.Graphics.FillRectangle(solidYellowBrush, x, y, w, h);
            //                        }
            //                        e.Graphics.DrawRectangle(borderPen, x, y, w, h);

            //                    }
            //                    y += ledRec.heigth;


            //                    break;
            //                }
            //            case "Folia":
            //                {
            //                    float x = marginLeft;
                                
            //                    float w = foilRec.width;
            //                    float h = foilRec.heigth;
            //                    if (packingPattern[i].Completed)
            //                    {
            //                        e.Graphics.FillRectangle(solidRedBrush, x, y, w, h);
            //                    }

            //                    e.Graphics.DrawRectangle(borderPen, x, y, w, h);
            //                    y += foilRec.heigth;
            //                    break;
            //                }
            //            case "Przekładka":
            //                {
            //                    float x = marginLeft;
                                
            //                    float w = spacerRec.width;
            //                    float h = spacerRec.heigth;
            //                    if (packingPattern[i].Completed)
            //                    {
            //                        e.Graphics.FillRectangle(solidBlackBrush, x, y, w, h);
            //                    }

            //                    e.Graphics.DrawRectangle(borderPen, x, y, w, h);
            //                    y += spacerRec.heigth;
            //                    break;
            //                }
            //        }
            //    }
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (var layer in packingPattern)
            {
                if (layer.Value.LayerName != "Warstwa wyrobów") continue;
                double allModules = layer.Value.ModuleQrCodes.Count;
                double completedModules = layer.Value.ModuleQrCodes.Select(m => m).Where(name => name != "").Count();

                if (completedModules == 0 || completedModules == allModules) continue;

                for (int i = 0; i < layer.Value.ModuleQrCodes.Count; i++) 
                {
                    if (layer.Value.ModuleQrCodes[i+1] =="")
                    {
                        layer.Value.ModuleQrCodes[i] = "";
                        pictureBox1.Image = Tools.DrawBitmap(packingPattern, pictureBox1);
                        break;
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.ActiveControl = textBox1;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            this.ActiveControl = textBox1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (FlexibleMessageBox.Show("Dane zostaną wyczyszczone!!", "Potwierdź", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {

                foreach (var item in packingPattern)
                {
                    item.Value.CompletionTime = new DateTime(1900, 01, 01);
                    item.Value.Completed = false;
                    if (item.Value.LayerName == "Warstwa wyrobów")
                    {
                        for (int i = 0; i < item.Value.ModuleQrCodes.Count; i++)
                        {
                            item.Value.ModuleQrCodes[i] = "";
                            item.Value.ModuleCompletitionDate[i] = new DateTime(1900, 01, 01);
                        }
                    }
                }
                Tools.UpdateFoilAndSpacerCompletition(packingPattern);
                pictureBox1.Image = Tools.DrawBitmap(packingPattern, pictureBox1);
            }
        }
    }
}
