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
        public string NickName { get; set; }
        public Stopwatch Score { get; set; }

        public User(string NickName)
        {
            this.NickName = NickName;
        }

        public User StartTime(User user)
        {
            if(user != null)
            {
                user.Score.Start();
            }
            return user;
        }
    }
}
