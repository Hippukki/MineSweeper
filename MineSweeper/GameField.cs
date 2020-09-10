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
    // Форма "Игровое поле"
    public partial class GameField : Form
    {
        Stopwatch stopwatch = new Stopwatch();
        int distancebetweenbuttons = 30;
        int width;
        int height;
        int countmines;
        int countcells;
        int ChosedLevel;
        bool FirstClick = false;
        List<Cell> cells = new List<Cell>();
        Cell[,] cellsfield;
        User user;
        UsersDB users;
        public GameField()
        {
            InitializeComponent();
        }
        public GameField(User user, int ChosedLevel)// Получаем пользователя из главной формы, определяем уровень сложности и запускаем секундомер
        {
            InitializeComponent();
            this.ChosedLevel = ChosedLevel;
            this.user = user;
            if (ChosedLevel == 0)
            {
                user.Difficult = "Easy";
                width = 10;
                height = 10;
                countmines = 10;
                countcells = 100;
            }
            else if(ChosedLevel == 1)
            {
                user.Difficult = "Middle";
                width = 15;
                height = 15;
                countmines = 40;
                countcells = 225;
            }
            else if(ChosedLevel == 2)
            {
                user.Difficult = "Hard";
                width = 20;
                height = 20;
                countmines = 90;
                countcells = 400;
            }
            stopwatch.Start();
        }

        private void GameField_Load(object sender, EventArgs e)
        {
            CellGeneration();
        }
        public void CellGeneration()// Создаём и разтавляем ячейки по полю
        {
            cellsfield = new Cell[width, height];// Инициализируем массив ячеек
            Random rnd = new Random();
            for (int y = 0; y < width; y++)
            {
                for (int x = 0; x < height; x++)
                {
                    Cell cell = new Cell(false, false, false);
                    cell.Location = new Point(x * distancebetweenbuttons, y * distancebetweenbuttons);
                    cell.Size = new Size(distancebetweenbuttons, distancebetweenbuttons);
                    if (rnd.Next(0, countcells) < countmines)// Делаем ячейки бомбами в рандомном порядке
                    {
                        cell.Bomb = true;
                    }
                    else
                    {
                        cell.Number = true;
                    }
                    cell.xCoord = x;// Присваеваем ячейкам их координаты
                    cell.yCoord = y;
                    Controls.Add(cell);// Добавляем ячейки в коллекцию элементов управления
                    cell.MouseUp += new MouseEventHandler(ClickOnCell);// Добавляем ячейкам событие "Нажатие на ячейку"
                    cells.Add(cell);// Добавляем ячейки в список
                    cellsfield[x, y] = cell;// А здесь в массив
                }
            }
        }

        void ClickOnCell(object sender, MouseEventArgs e)// Событие "Нажатие на кнопку"
        {
            Cell cell = (Cell)sender;
            if (e.Button == MouseButtons.Left && cell.IsFlag == false)// Нажали на левую кнопку
            {
                if (cell.Bomb == true)// Ячейка = бомба
                {
                    if(FirstClick == false)// Если это первое нажатие с начала игры
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
                else if (cell.Number == true)// Ячейка != бомба
                {
                    FirstClick = true;
                    OpeningEmptyCells(cell.xCoord, cell.yCoord, cell);// Открываем ячейку
                }
            }
            else if (e.Button == MouseButtons.Right)// Нажали на правую кнопку
            {
                if(cell.IsFlag == false)// Если нет флага
                {
                    cell.IsFlag = true;
                    cell.IsOpen = true;
                    cell.Image = Resources.Flag;
                }
                else if(cell.IsFlag == true)// Если есть
                {
                    cell.IsFlag = false;
                    cell.IsOpen = false;
                    cell.Image = Resources.BUTTON;
                }
            }
            CheckWin();// Проверка сколько ячеек открыто
        }
        public void Boom()// Взрыв при нажатии на бомбу
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
            new BadPlayWindow(user, ChosedLevel).ShowDialog();// Открываем форму "Вы проиграли" и передаём туда пользователя
        }
        int CountBombsAround(int abscissa, int ordinate)// Подсчитывает сколько бомб вокруг ячейки с координатами, которые мы передали в метод
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
        public void StopTime()// Останавливаем секундомер, присваеваем значение времени пользователю и сохраняем его в коллекции
        {
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            user.Time = String.Format("{1:00}:{2:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            users = UsersDB.GetInstance();
            users.Save(user);
        }
        void OpeningEmptyCells(int abscissa, int ordinate, Cell cell)// Открывает ячейку и все соседние, если они пустые
        {
            Queue<Cell> queue = new Queue<Cell>();
            queue.Enqueue(cell);// Добавляем ячейку в очередь
            while (queue.Count > 0)
            {
                Cell cell1 = queue.Dequeue();// Извлекаем ячейку из очереди
                OpenCell(cell1.xCoord, cell1.yCoord, cell1);// Открываем её
                if (CountBombsAround(cell1.xCoord, cell1.yCoord) == 0)
                {
                    for (int y = cell1.yCoord - 1; y <= cell1.yCoord + 1; y++)
                    {
                        for (int x = cell1.xCoord - 1; x <= cell1.xCoord + 1; x++)// если бомб вокруг ноль, то добавляем соседние ячейки и проверяем наличие бомб вокруг них
                        {
                            if (x >= 0 && x < height && y >= 0 && y < width)// Устанавливаем ограничение, чтобы не выйти за границы игрового поля
                            {
                                if (!cellsfield[x, y].WasAdded)// Проверяем была эта ячейка уже проверена ранее или нет
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
        void OpenCell(int x, int y, Cell cell)//Присваеваем ячейке значение количества бомб вокруг неё 
        {
            cell.IsOpen = true;
            int bombsAround = CountBombsAround(x, y);
            if (bombsAround == 0)
            {
                cell.Image = Resources.Nothing;//Ничего
            }
            else if (bombsAround == 1)
            {
                cell.Image = Resources.One;//Один
            }
            else if (bombsAround == 2)
            {
                cell.Image = Resources.Two;//Два
            }
            else if (bombsAround == 3)
            {
                cell.Image = Resources.three;//Три
            }
            else if (bombsAround == 4)
            {
                cell.Image = Resources.four;//Четыре
            }
            else if (bombsAround == 5)
            {
                cell.Image = Resources.five;//Пять
            }
        }
        void CheckWin()//Проверяем сколько ячеек открыто
        {
            int wincount = 0;
            foreach(Cell cell in cells)
            {
                if(cell.IsOpen == true)
                {
                    wincount++;
                }
            }
            if(wincount == width*height)//Если открыты все, то останавливаем время и открываем форму "Вы победили"
            {
                StopTime();
                new GoodPlayWindow().ShowDialog();
                this.Close();
            }
        }

    }
}
