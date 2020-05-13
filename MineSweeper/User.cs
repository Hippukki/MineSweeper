using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper
{
    [Serializable]
    public class User
    {
        public int ID { get; set; }
        public string NickName { get; set; }
        public DateTime Timer { get; set; }

        public User(int ID)
        {
            this.ID = ID;
        }
        public User(string NickName, DateTime Timer)
        {
            this.NickName = NickName;
            this.Timer = Timer;

        }
    }
}
