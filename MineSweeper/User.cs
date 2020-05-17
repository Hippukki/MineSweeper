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
        // Класс пользователя
        public int ID { get; set; }
        public string NickName;
        public string Time;
        public string Difficult;

        public User(int ID)
        {
            this.ID = ID;
        }
        public User(string NickName, string Time, string Difficult)
        {
            this.NickName = NickName;
            this.Time = Time;
            this.Difficult = Difficult;
        }
    }
}
