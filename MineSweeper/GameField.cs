using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        Stopwatch stopwatch = new Stopwatch();
        int distancebetweenbuttons = 30;
        int width = 10;
        int height = 10;
        bool FirstClick = false;
        List<Cell> cells = new List<Cell>();
        Cell[,] cellsfield;
        int wincount = 0;
        public User user;
        public GameField()
        {
            InitializeComponent();
        }
        public GameField(User user)
        {
            InitializeComponent();
            this.user = user;
            stopwatch.Start();
        }

        private void GameField_Load(object sender, EventArgs e)
        {
            CellGeneration();
        }
        public void CellGeneration()
        {
            cellsfield = new Cell[width, height];
            Random rnd = new Random();
            for (int y = 0; y < width; y++)
            {
                for (int x = 0; x < height; x++)
                {
                    Cell cell = new Cell(false, false, false);
                    cell.Location = new Point(x * distancebetweenbuttons, y * distancebetweenbuttons);
                    cell.Size = new Size(distancebetweenbuttons, distancebetweenbuttons);
                    if (rnd.Next(0, 100) < 20)
                    {
                        cell.Bomb = true;
                    }
                    else
                    {
                        cell.Number = true;
                    }
                    cell.xCoord = x;
                    cell.yCoord = y;
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
            if (e.Button == MouseButtons.Left && cell.IsFlag == false)
            {
                if (cell.Bomb == true)
                {
                    if(FirstClick == false)
                    {
                        cell.Bomb = false;
                        cell.Number = true;
                        FirstClick = true;
                    }
                    else
                    {
                        Boom();
                        this.Close();
                    }
                }
                else if (cell.Number == true)
                {
                    FirstClick = true;
                    OpeningEmptyCells(cell.xCoord, cell.yCoord, cell);
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                if(cell.IsFlag == false)
                {
                    cell.IsFlag = true;
                    cell.IsOpen = true;
                    cell.Image = Resources.Flag;
                }
                else if(cell.IsFlag == true)
                {
                    cell.IsFlag = false;
                    cell.IsOpen = false;
                    cell.Image = Resources.BUTTON;
                }
            }
            CheckWin();
        }
        public void Boom()
        {
            foreach (Cell cell in cells)
            {
                if (cell.Bomb == true)
                {
                    cell.Image = Resources.Bomb;
                }
                else
                {
                    OpeningEmptyCells(cell.xCoord, cell.yCoord, cell);
                }
            }
            StopTime();
            new BadPlayWindow().ShowDialog();
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
                        if (cellsfield[x, y].Bomb == true)
                        {
                            bombsAround++;
                        }
                    }
                }
            }
            return bombsAround;
        }
        public void StopTime()
        {
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            user.Time = String.Format("{1:00}:{2:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
        }
        void OpeningEmptyCells(int abscissa, int ordinate, Cell cell)
        {
            Queue<Cell> queue = new Queue<Cell>();
            queue.Enqueue(cell);
            while (queue.Count > 0)
            {
                Cell cell1 = queue.Dequeue();
                OpenCell(cell1.xCoord, cell1.yCoord, cell1);
                if (CountBombsAround(cell1.xCoord, cell1.yCoord) == 0)
                {
                    for (int y = cell1.yCoord - 1; y <= cell1.yCoord + 1; y++)
                    {
                        for (int x = cell1.xCoord - 1; x <= cell1.xCoord + 1; x++)
                        {
                            if (x >= 0 && x < height && y >= 0 && y < width)
                            {
                                if (!cellsfield[x, y].WasAdded)
                                {
                                    queue.Enqueue(cellsfield[x, y]);
                                    cellsfield[x, y].WasAdded = true;
                                }
                            }
                        }
                    }
                }
            }
        }
        void OpenCell(int x, int y, Cell cell)
        {
            cell.IsOpen = true;
            int bombsAround = CountBombsAround(x, y);
            if (bombsAround == 0)
            {
                cell.Image = Resources.Nothing;
            }
            else if (bombsAround == 1)
            {
                cell.Image = Resources.One;
            }
            else if (bombsAround == 2)
            {
                cell.Image = Resources.Two;
            }
            else if (bombsAround == 3)
            {
                cell.Image = Resources.three;
            }
            else if (bombsAround == 4)
            {
                cell.Image = Resources.four;
            }
            else if (bombsAround == 5)
            {
                cell.Image = Resources.five;
            }
        }
        void CheckWin()
        {
            wincount = 0;
            foreach(Cell cell1 in cells)
            {
                if(cell1.IsOpen == true)
                {
                    wincount++;
                }
            }
            if(wincount == width*height)
            {
                new GoodPlayWindow().ShowDialog();
            }
        }

    }
}
