using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class DataMethodsUser
    {
        public DataMethodsUser()
        {
            if(!GetAllDataUsers().Any() || !GetAllDataUsers().ToList().Exists(user => user.Role.Equals("Admin")))
            {
                AddUser(new User
                {
                    Id = GetAllDataUsers().Any() ? GetAllDataUsers().Last().Id + 1 : 0,
                    Login = "Admin",
                    Password = "1234",
                    Role = "Admin"
                });
            }
        }

        string path = @"C:\Users\1\source\repos\WpfApp1\WpfApp1\Data\Users.txt";
        public void AddUser(User user)
        {
            if(user != null)
            {
                using (StreamWriter sr = new StreamWriter(path, true))
                {
                    sr.WriteLine(JsonSerializer.Serialize<User>(user));
                }
            }
        }
       
        public void RewriteAllUsers(List<User> users)
        {
            if (users != null && users.Any())
            {
                File.Delete(path);
                using (StreamWriter sr = new StreamWriter(path, true))
                {
                    users.ForEach(user => sr.WriteLine(JsonSerializer.Serialize<User>(user)));
                }
            }
        }

        public void EditUserProfile() { }

        public IEnumerable<User> GetAllDataUsers()
        {
            var list = new List<User>();
            
            using(StreamReader sr = new StreamReader(path))
            {
                string line;
                while (!string.IsNullOrEmpty((line = sr.ReadLine())))
                {
                    list.Add(JsonSerializer.Deserialize<User>(line));
                }
            }

            return list;
        }
    }
}
