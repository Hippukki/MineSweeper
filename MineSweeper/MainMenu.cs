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
    public partial class MainMenu : Form
    {
        // Главная форма (Игровое меню)
        int ChosedLevel { get; set; }
        public MainMenu()
        {
            InitializeComponent();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            
        }

        private void Button1_Click(object sender, EventArgs e)// Создаём пользователя, даём ему имя из текстбокса и передаём в форму "Игровое поле"
        {
            var user = UsersDB.GetInstance().CreateUser();
            user.NickName = textBox1.Text;
            if(user.NickName == null)
            {
                user.NickName = "NickName";
            }
            Hide();
            new GameField(user, ChosedLevel).ShowDialog();
        }

        private void TextBox1_Click(object sender, EventArgs e)// Очищаем текстбокс от текста при нажатии на него
        {
            textBox1.Clear();
        }

        private void Button2_Click(object sender, EventArgs e)// Открываем форму "Таблица рекордов"
        {
            new HighScoreTable().ShowDialog();
        }

        private void MainMenu_FormClosed(object sender, FormClosedEventArgs e)// Закрываем приложение при закрытии главной формы
        {
            Application.Exit();
        }

        private void MiddleToolStripMenuItem_Click(object sender, EventArgs e)// Средний уровень сложности
        {
            ChosedLevel = 1;
            levelOfDifficultyToolStripMenuItem.Text = "Middle";
        }

        private void HardToolStripMenuItem_Click(object sender, EventArgs e)// Тяжелый уровень сложности
        {
            ChosedLevel = 2;
            levelOfDifficultyToolStripMenuItem.Text = "Hard";
        }

        private void EasyToolStripMenuItem_Click(object sender, EventArgs e)// Легкий уровень сложности
        {
            ChosedLevel = 0;
            levelOfDifficultyToolStripMenuItem.Text = "Easy";
        }
    }
}
