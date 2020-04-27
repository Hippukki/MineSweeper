using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineSweeper
{
    public class Number : Button

    {
        public string Colour { get; set; }
        public int Value { get; set; }

        public Number(string Colour, int Value)
        {
            this.Colour = Colour;
            this.Value = Value;
        }
  
        
        
    }
}
