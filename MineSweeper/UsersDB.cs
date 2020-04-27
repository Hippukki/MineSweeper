using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper
{
    [Serializable]
    class UsersDB
    {
        Dictionary<string, User> users;
        Serializer<Dictionary<string, User>> serializer;
        int autoincrement = 1;
        string filePath;
        static UsersDB instance;
        
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
            filePath = "status.bin";
            serializer = new Serializer<Dictionary<string, User>>(filePath);
            users = serializer.Load(ref autoincrement);
        }

        public void Save() => serializer.Save(users, autoincrement);

        public User CreateUser(string NickName)
        {
            User user = new User(NickName);
            user.StartTime(user);
            users.Add(user.NickName, user);
            return user;
        }

        //public User SaveUserScore(string NickName) { }

    }
}
