using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pakowanie_LED
{
    class Tools
    {
        public static void ResizeGrid(DataGridView grid)
        {
            if (grid.Rows.Count > 0 & grid.Columns.Count > 0)
            {
                grid.SuspendLayout();
                decimal colWidth = Math.Truncate((decimal)((decimal)grid.Width / (decimal)grid.Columns.Count));
                decimal rowHeigth = Math.Truncate((decimal)((decimal)grid.Height / (decimal)grid.Rows.Count));
                foreach (DataGridViewColumn col in grid.Columns)
                {
                    col.Width = (int)colWidth;
                }
                foreach (DataGridViewRow row in grid.Rows)
                {
                    row.Height = (int)rowHeigth;
                }

                grid.ResumeLayout();
            }
        }


        public static void UpdateFoilAndSpacerCompletition(Dictionary<int, PackingLayers> packingLayers)
        {
            int result = 0;

            for (int i = packingLayers.Count - 1; i > -1; i--)
            {
                if (packingLayers[i].LayerName == "Warstwa wyrobów")
                {
                    if (!packingLayers[i].Completed) 
                    {
                        if (i == packingLayers.Count - 1) result = i;
                        else result = i + 1;
                        break;
                    }
                }
            }

            for (int i = packingLayers.Count - 1; i >= result; i--)
            {
                if (packingLayers[i].LayerName != "Warstwa wyrobów" & !packingLayers[i].Completed)
                {
                    packingLayers[i].Completed = true;
                    FlexibleMessageBox.Show("Teraz wastwa: " + packingLayers[i].LayerName, "Nowa warstwwa");
                }
            }
        }

        public static double CountLedPanels(Dictionary<int, PackingLayers> packingLayers)
        {
            double result = 0;

            for (int i = packingLayers.Count - 1; i > -1; i--)
            {
                if (packingLayers[i].LayerName == "Warstwa wyrobów")
                {
                    double count = packingLayers[i].ModuleQrCodes.Where(l => l != "").Count();
                    result += count;
                }
            }
                return result;
        }
        class Rectangle
        {
            public float width;
            public float heigth;
        }
        public static Bitmap DrawBitmap(Dictionary<int, PackingLayers> packingPattern, PictureBox panel2)
        {
            Bitmap result = new Bitmap(panel2.Width, panel2.Height);

            if (packingPattern.Count > 0)
            {
                int numberOfFoilLayers = packingPattern.Select(s => s.Value).Where(w => w.LayerName.Contains("Foli")).Count();
                int numberOfSpacersLayer = packingPattern.Select(s => s.Value).Where(w => w.LayerName.Contains("Przek")).Count();
                int numberOfLedLayers = packingPattern.Select(s => s.Value).Where(w => w.LayerName.Contains("wyrob")).Count();
                int numberOfAllLayers = packingPattern.Count;

                int canvasWidth = panel2.Width;
                int canvasHeigth = panel2.Height;

                int marginLeft = 2;
                int marginRight = 2;
                int marginTop = 2;
                int marginBottom = 2;

                Rectangle foilRec = new Rectangle();
                Rectangle spacerRec = new Rectangle();
                Rectangle ledRec = new Rectangle();

                int numberOfLedPerLayer = 0;
                foreach (var item in packingPattern)
                {
                    if (item.Value.LayerName == "Warstwa wyrobów")
                    {
                        numberOfLedPerLayer = item.Value.ModuleQrCodes.Count;
                        break;
                    }
                }

                foilRec.width = canvasWidth - marginLeft - marginRight;
                foilRec.heigth = (canvasHeigth - marginTop - marginBottom) / numberOfAllLayers * (float)0.7;

                spacerRec.width = canvasWidth - marginLeft - marginRight;
                spacerRec.heigth = (canvasHeigth - marginTop - marginBottom) / numberOfAllLayers * (float)0.7;

                ledRec.width = (canvasWidth - marginLeft - marginRight) / numberOfLedPerLayer;
                ledRec.heigth = (canvasHeigth - marginTop - marginBottom - foilRec.heigth * numberOfFoilLayers - spacerRec.heigth * numberOfSpacersLayer) / numberOfLedLayers;

                Pen borderPen = new Pen(Color.Black, 1);
                System.Drawing.SolidBrush solidGreenBrush = new System.Drawing.SolidBrush(System.Drawing.Color.LightGreen);
                System.Drawing.SolidBrush solidBlackBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
                System.Drawing.SolidBrush solidRedBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
                float y = marginTop;
                using (Graphics e = Graphics.FromImage(result))
                {
                    e.Clear(Color.White);
                    for (int i = 0; i < packingPattern.Count; i++)
                    {




                        switch (packingPattern[i].LayerName)
                        {
                            case "Warstwa wyrobów":
                                {


                                    float w = ledRec.width;
                                    float h = ledRec.heigth;

                                    for (int led = 0; led < packingPattern[i].ModuleQrCodes.Count; led++)
                                    {
                                        float x = marginLeft + led * ledRec.width;
                                        if (packingPattern[i].ModuleQrCodes[led] != "")
                                        {
                                            e.FillRectangle(solidGreenBrush, x, y, w, h);
                                        }
                                        e.DrawRectangle(borderPen, x, y, w, h);

                                    }
                                    y += ledRec.heigth;


                                    break;
                                }
                            case "Folia":
                                {
                                    float x = marginLeft;

                                    float w = foilRec.width;
                                    float h = foilRec.heigth;
                                    if (packingPattern[i].Completed)
                                    {
                                        e.FillRectangle(solidRedBrush, x, y, w, h);
                                    }

                                    e.DrawRectangle(borderPen, x, y, w, h);
                                    y += foilRec.heigth;
                                    break;
                                }
                            case "Przekładka":
                                {
                                    float x = marginLeft;

                                    float w = spacerRec.width;
                                    float h = spacerRec.heigth;
                                    if (packingPattern[i].Completed)
                                    {
                                        e.FillRectangle(solidBlackBrush, x, y, w, h);
                                    }

                                    e.DrawRectangle(borderPen, x, y, w, h);
                                    y += spacerRec.heigth;
                                    break;
                                }
                        }
                    }
            }

            
                
            }

            return result;
        }

        public static double CountModulesPerHour(Dictionary<int, PackingLayers> packingLayers)
        {
            double result = 0;
            DateTime zeroDate = new DateTime(1900, 01, 01);

            double timePassedSinceFirst = (System.DateTime.Now-packingLayers.SelectMany(s => s.Value.ModuleCompletitionDate.Where(d=>d>zeroDate)).Min()).TotalMinutes;
            

            if (timePassedSinceFirst>=60)
            {
                result = packingLayers.SelectMany(s => s.Value.ModuleCompletitionDate.Where(d => d > zeroDate)).Count()/(timePassedSinceFirst/60);
            }
            else
            {
                result = packingLayers.SelectMany(s => s.Value.ModuleCompletitionDate.Where(d => d > zeroDate)).Count() / (timePassedSinceFirst / 60);
            }

            return result;
        }

        public static void AddnewQr(Dictionary<int, PackingLayers> packingLayers, string qrCode)
        {
            bool panelAdded = false;
            UpdateFoilAndSpacerCompletition(packingLayers);

            for (int i = packingLayers.Count - 1; i > -1; i--)
            {
                if (packingLayers[i].LayerName == "Warstwa wyrobów" & !packingLayers[i].Completed)
                {
                    for (int k = 0; k < packingLayers[i].ModuleQrCodes.Count; k++)
                    {
                        if (packingLayers[i].ModuleQrCodes[k] == "")
                        {
                            packingLayers[i].ModuleQrCodes[k] = qrCode;
                            packingLayers[i].ModuleCompletitionDate[k] = System.DateTime.Now;
                            
                            if (k == packingLayers[i].ModuleQrCodes.Count - 1)
                            {
                                packingLayers[i].Completed = true;
                                Tools.UpdateFoilAndSpacerCompletition(packingLayers);
                            }
                            panelAdded = true;
                            break;
                        }
                    }
                }
                if (panelAdded) break;
            }
        }

        public static void UpdateLayerColors(DataGridView grid, Dictionary<int, PackingLayers> packingLayers)
        {
            //grid.SuspendLayout();
            //UpdateFoilAndSpacerCompletition(packingLayers);

            //for (int i = packingLayers.Count - 1; i > -1; i--) 
            //{
            //    if (packingLayers[i].LayerName == "Warstwa wyrobów")
            //    {
            //        for (int j = 0; j < packingLayers[i].ModuleQrCodes.Count; j++)
            //        {
            //            if (packingLayers[i].ModuleQrCodes[j] != "")
            //                grid.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.Yellow;
            //        }
            //    }
            //    else
            //    {
            //        if (packingLayers[i].LayerName == "Przekładka" & i >= maxFoilOrSpacerIndex)
            //            foreach (DataGridViewCell cell in grid.Rows[i].Cells)
            //            {
            //                cell.Style.BackColor = System.Drawing.Color.Black;
            //            }


            //        if (packingLayers[i].LayerName == "Folia" & i >= maxFoilOrSpacerIndex)
            //            foreach (DataGridViewCell cell in grid.Rows[i].Cells)
            //            {
            //                cell.Style.BackColor = System.Drawing.Color.Red;
            //            }
            //    }
                
            //}
            //grid.ResumeLayout();
        }

        


    }
}
