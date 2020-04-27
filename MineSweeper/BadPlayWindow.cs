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
    public partial class BadPlayWindow : Form
    {
        public BadPlayWindow()
        {
            InitializeComponent();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            var mm = new MainMenu();
            mm.Show();
            this.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var gf = new GameField();
            gf.Show();
            this.Close();
        }
    }
}
