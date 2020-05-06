using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineSweeper
{
    public partial class GameField : Form
    {
        int distancebetweenbuttons = 30;
        int countbomb = 0;
        int width = 10;
        int height = 10;
        List<Cell> cells = new List<Cell>();
        public GameField()
        {
            InitializeComponent();
        }

        private void GameField_Load(object sender, EventArgs e)
        {
            Random rnd = new Random();
            for (int x = 10; (x - 10) < width * distancebetweenbuttons; x += distancebetweenbuttons)
            {
                for (int y = 10; (y - 10) < height * distancebetweenbuttons; y += distancebetweenbuttons)
                {
                    Cell cell = new Cell(false, null, null);
                    cell.Location = new Point(x, y);
                    cell.Size = new Size(30, 30);
                    if (rnd.Next(0, 101) < 20)
                    {
                        cell.Bomb = new Bomb(false, countbomb++);
                    }
                    else
                    {
                        cell.Number = new Number("", 0);
                    }
                    Controls.Add(cell);
                    cell.MouseUp += new MouseEventHandler(ClickOnCell);
                    cells.Add(cell);
                }
            }

        }

        void ClickOnCell(object sender, MouseEventArgs e)
        {
            Cell cell = (Cell)sender;
            if(e.Button == MouseButtons.Left)
            {
                if (cell.Bomb != null)
                {
                    Boom();
                    cell.Bomb.IsBlown = true;
                    this.Close();
                }
                else if (cell.Number != null)
                {
                    ClickingEmptyCell(cell);
                    cell.IsOpen = true;
                }
            }
            else if(e.Button == MouseButtons.Right)
            {

            }
        }
        public void Boom()
        {
            foreach (Cell cell in cells)
            {
                if (cell.Bomb != null)
                {
                    string col = "Red";
                    cell.BackColor = Color.FromName(col);
                }
            }
            new BadPlayWindow().ShowDialog();
        }
        public void ClickingEmptyCell(Cell cell)
        {

        }
        public void CountBombAround(Cell cell)
        {
                       
        }
    }

    
}
