using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineSweeper
{
    public class Cell : Button
    {
        public bool Number { get; set; }
        public bool IsOpen;
        public bool IsFlag;
        public int xCoord;
        public int yCoord;
        public bool WasAdded;
        public bool Bomb { get; set; }

        public Cell( bool IsOpen, bool Number, bool Bomb)
        {
            this.IsOpen = IsOpen;
            this.Number = Number;
            this.Bomb = Bomb;
        }

    }
}
