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
    // Форма "Вы проиграли"
    public partial class BadPlayWindow : Form
    {
        int ChosedLevel;
        User user;
        public BadPlayWindow()
        {
            InitializeComponent();
        }
        public BadPlayWindow(User user, int ChosedLevel)//Получаем пользователя и уровень сложности из формы "Игровое поле"
        {
            InitializeComponent();
            this.ChosedLevel = ChosedLevel;
            this.user = user;
        }

        private void Button2_Click(object sender, EventArgs e)//Выходим в главное меню
        {
            var mm = new MainMenu();
            mm.Show();
            this.Close();
        }

        private void Button1_Click(object sender, EventArgs e)//Или начинаем заново 
        {
            var gf = new GameField(user, ChosedLevel);
            gf.Show();
            this.Close();
        }
    }
}
