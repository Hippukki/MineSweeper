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
    // Форма "Вы победили"
    public partial class GoodPlayWindow : Form
    {
        public GoodPlayWindow()
        {
            InitializeComponent();
        }

        private void GoodPlayWindow_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)// Возвращяемся в главное меню
        {
            var mm = new MainMenu();
            mm.Show();
            this.Close();
        }
    }
}
