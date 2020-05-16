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
        public MainMenu()
        {
            InitializeComponent();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var user = UsersDB.GetInstance().CreateUser();
            user.NickName = textBox1.Text;
            if(user.NickName == null)
            {
                user.NickName = "NickName";
            }
            Hide();
            new GameField(user).ShowDialog();
        }

        private void TextBox1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            new HighScoreTable().ShowDialog();
        }
    }
}
