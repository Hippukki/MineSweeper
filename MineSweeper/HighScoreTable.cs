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
    public partial class HighScoreTable : Form
    {
        public HighScoreTable()
        {
            InitializeComponent();
            ShowListUsers();
        }
        void ShowListUsers()
        {
            listView1.Items.Clear();
            var users = UsersDB.GetInstance().
                GetUsers();
            foreach (var user in users)
            {
                ListViewItem row = new ListViewItem(
                    user.NickName);
                row.SubItems.Add(user.Time);
                row.Tag = user;
                listView1.Items.Add(row);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            
        }
    }
}
