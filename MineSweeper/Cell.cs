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
        public Number Number { get; set; }
        public bool IsOpen;
        public Bomb Bomb { get; set; }

        public Cell( bool IsOpen, Number Number, Bomb Bomb)
        {
            this.IsOpen = IsOpen;
            this.Number = Number;
            this.Bomb = Bomb;
        }

        public void Opening(Cell cell)
        {

        }

    }
}
