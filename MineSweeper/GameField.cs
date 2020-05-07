using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MineSweeper.Properties;

namespace MineSweeper
{
    public partial class GameField : Form
    {
        int distancebetweenbuttons = 30;
        int countbomb = 0;
        int width = 10;
        int height = 10;
        List<Cell> cells = new List<Cell>();
        Cell[,] cellsfield;
        public GameField()
        {
            InitializeComponent();
        }

        private void GameField_Load(object sender, EventArgs e)
        {
            cellsfield = new Cell[width, height];
            Random rnd = new Random();
            for (int y = 0; y < width; y++)
            {
                for (int x = 0; x < height; x++)
                {
                    Cell cell = new Cell(false, null, null);
                    cell.Location = new Point(x * distancebetweenbuttons, y * distancebetweenbuttons);
                    cell.Size = new Size(distancebetweenbuttons, distancebetweenbuttons);
                    if (rnd.Next(0, 100) < 20)
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
                    cellsfield[x, y] = cell;
                }
            }

        }

        void ClickOnCell(object sender, MouseEventArgs e)
        {
            Cell cell = (Cell)sender;
            if (e.Button == MouseButtons.Left)
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
            else if (e.Button == MouseButtons.Right)
            {
                cell.Image = Resources.Flag;
            }
        }
        public void Boom()
        {
            foreach (Cell cell in cells)
            {
                if (cell.Bomb != null)
                {
                    cell.Image = Resources.Bomb;
                }
                else
                {
                    ClickingEmptyCell(cell);
                }
            }
            new BadPlayWindow().ShowDialog();
        }
        public void ClickingEmptyCell(Cell cell)
        {
            int bombsAround = 0;
            for (int y = 0; y < width; y++)
            {
                for (int x = 0; x < height; x++)
                {
                    if (cellsfield[x, y] == cell)
                    {
                        bombsAround = CountBombsAround(x, y);
                    }
                }
            }
            if (bombsAround == 0)
            {
                cell.Image = Resources.Nothing;
            }
            else if(bombsAround == 1)
            {
                cell.Image = Resources.One;
            }
            else if(bombsAround == 2)
            {
                cell.Image = Resources.Two;
            }
            else if(bombsAround == 3)
            {
                cell.Image = Resources.three;
            }
            else if(bombsAround == 4)
            {
                cell.Image = Resources.four;
            }
        }
        int CountBombsAround(int abscissa, int ordinate)
        {
            int bombsAround = 0;
            for (int x = abscissa - 1; x <= abscissa + 1; x++)
            {
                for (int y = ordinate - 1; y <= ordinate + 1; y++)
                {
                    if (x >= 0 && x < height && y >= 0 && y < width)
                    {
                        if (cellsfield[x, y].Bomb != null)
                        {
                            bombsAround++;
                        }
                    }
                }
            }
            return bombsAround;
        }



    }
}
