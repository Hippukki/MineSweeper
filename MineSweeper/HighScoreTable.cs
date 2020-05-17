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
    // Форма "Таблица рекордов"
    public partial class HighScoreTable : Form
    {
        public HighScoreTable()
        {
            InitializeComponent();
            ShowListUsers();
        }
        void ShowListUsers()//Сортируем пользователей в коллекции по затраченому времени на катку(по возрастанию) и заполняем ими таблицу
        {
            listView1.Items.Clear();
            var users = UsersDB.GetInstance().
                GetUsers();
            var sortedUsers = from u in users
                              orderby u.Time
                              select u;
            foreach (var u in sortedUsers)
            {
                    ListViewItem row = new ListViewItem(
                        u.NickName);
                    row.SubItems.Add(u.Time);
                row.SubItems.Add(u.Difficult);
                    listView1.Items.Add(row);
            }
        }

        private void Button1_Click(object sender, EventArgs e)// Очищаем таблицу и коллекцию, чтобы не накапливалось слишком много пользователей
        {
            listView1.Items.Clear();
            var users = UsersDB.GetInstance();
            users.ClearUsers();
            
        }
    }
}
