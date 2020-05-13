using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        Dictionary<int, User> users;
        Serializer<Dictionary<int, User>> serializer;
        string filePath;
        int autoincrement = 1;

        static UsersDB instance;
        public static UsersDB GetInstance()
        {
            if (instance == null)
                instance = new UsersDB();
            return instance;
        }

        private UsersDB()
        {
            filePath = "workers.bin";
            serializer = new Serializer<
                Dictionary<int, User>>(filePath);
            users = serializer.Load(ref autoincrement);
        }

        public void Save()
        {
            serializer.Save(users, autoincrement);
        }

        public User CreateUser()
        {
            var user = new User(autoincrement++);
            users.Add(user.ID, user);
            return user;
        }
        public List<User> GetUsers()
        {
            return users.
                Select(s => s.Value)?.ToList();
        }
    }
}
