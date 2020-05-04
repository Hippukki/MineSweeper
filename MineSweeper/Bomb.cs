using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineSweeper
{
    public class Bomb : Button
    {
        public bool IsBlown { get; set; }
        public int Count { get; set; }

        public Bomb(bool IsBlown, int Count)
        {
            this.IsBlown = IsBlown;
            this.Count = Count;
        }


    }
}
