using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper
{
    public class GameBoard
    {
        public Cell cells { get; set; }
        public int BombCount { get; set; }
        public DateTime time { get; set; }
    }
}
