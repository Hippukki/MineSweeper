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
        public string NickName;
        public string Time;

        public User(int ID)
        {
            this.ID = ID;
        }
    }
}
