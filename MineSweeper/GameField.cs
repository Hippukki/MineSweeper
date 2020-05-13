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
        int distancebetweenbuttons = 30;
        int countbomb = 0;
        int width = 10;
        int height = 10;
        List<Cell> cells = new List<Cell>();
        Cell[,] cellsfield;
        public User user;
        public GameField()
        {
            InitializeComponent();
        }
        public GameField(User user)
        {
            InitializeComponent();
            this.user = user;
            start_timer(user);


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
                if (cell.Bomb != null)
                    cell.Bomb = null;
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
            else if(bombsAround == 5)
            {
                cell.Image = Resources.five;
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
        public void start_timer(User user)
        {
            user.Timer = DateTime.Now;

            Timer timer = new Timer();
            timer.Interval = 10;
            timer.Tick += new EventHandler(tickTimer);
            timer.Start();
        }

        public void tickTimer(object sender, EventArgs e)
        {
            long tick = DateTime.Now.Ticks - user.Timer.Ticks;
            DateTime stopWatch = new DateTime();

            stopWatch = stopWatch.AddTicks(tick);
            label1.Text = String.Format("{0:mm:ss}", stopWatch);
        }

    }
}
