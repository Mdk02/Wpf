
using System;

namespace WpfApp1.Models
{
    public class User
    {
        public int Id { get; set; }
        //Mail
        public string Login { get; set; }
        public string Password { get; set; }
        public string Lastname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string TelNumber { get; set; }
        public DateTime BirthDate { get; set; }
        
        //user, rieltor, admin   add ban
        public string Role { get; set; }
    }
}
