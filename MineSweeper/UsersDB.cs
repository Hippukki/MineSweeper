using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper
{
    [Serializable]
    class UsersDB
    {
        Dictionary<string, User> users;
        int autoincrement = 1;
        string filePath;
        static UsersDB instance;
        BinaryFormatter formatter;
        
        public static UsersDB GetInstance()
        {
            if(instance == null)
            {
                instance = new UsersDB();
            }
            return instance;
        }

        private UsersDB()
        {
            filePath = "users.bin";
            formatter = new BinaryFormatter();
            using(FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, users);
            }
        }

        public void Save()
        {

        }

        public User CreateUser(string NickName)
        {
            User user = new User(NickName);
            user.StartTime(user);
            users.Add(user.NickName, user);
            return user;
        }

    }
}
