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
                decimal colWidth = Math.Truncate((decimal)((decimal)grid.Width / (decimal)grid.Columns.Count));
                foreach (DataGridViewColumn col in grid.Columns)
                {
                    col.Width = (int)colWidth;
                }
            }
        }
    }
}
