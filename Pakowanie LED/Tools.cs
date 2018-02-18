using System;
using System.Collections.Generic;
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

        public static void NewQrScanned(Dictionary<int, PackingLayers> packingLayers, string qrCode)
        {
            bool panelAdded = false;
            for (int i = packingLayers.Count - 1; i > -1; i--)
            {
                if (panelAdded) break;
                else
                {
                    if (!packingLayers[i].Completed)
                    {
                        if (packingLayers[i].LayerName != "Warstwa wyrobów")
                        {

                            for (int x = 0; x < packingLayers[i].ModuleQrCodes.Count; x++)
                            {
                                packingLayers[i].ModuleQrCodes[x] = packingLayers[i].LayerName;
                            }
                            packingLayers[i].Completed = true;
                            FlexibleMessageBox.Show("Połóż teraz " + packingLayers[i].LayerName);
                            continue;
                        }
                        else
                        {
                            if (qrCode != "")
                            {
                                for (int pcb = 0; pcb < packingLayers[i].ModuleQrCodes.Count; pcb++)
                                {
                                    
                                    if (packingLayers[i].ModuleQrCodes[pcb] != "") continue;
                                    packingLayers[i].ModuleQrCodes[pcb] = qrCode;
                                    panelAdded = true;
                                    if (i == packingLayers[i].ModuleQrCodes.Count - 1)
                                    {
                                        packingLayers[i].Completed = true;
                                    }
                                    if(i == packingLayers[i].ModuleQrCodes.Count - 1 ) packingLayers[i].Completed = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }


        }

        public static void UpdateLayerColors(DataGridView grid, Dictionary<int, PackingLayers> packingLayers)
        {
            for (int i = packingLayers.Count - 1; i > -1; i--) 
            {
                for (int j=0;j<packingLayers[i].ModuleQrCodes.Count;j++)
                {
                    if (packingLayers[i].ModuleQrCodes[j]!="")
                    {
                        switch (packingLayers[i].LayerName)
                        {
                            case "Warstwa wyrobów":
                                grid.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.Yellow;
                                break;
                            case "Przekładka":
                                grid.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.Black;
                                break;
                            case "Folia":
                                grid.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.Red;
                                break;
                        }

                    }
                }
            }
        }

        


    }
}
