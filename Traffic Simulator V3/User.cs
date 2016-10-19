using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traffic_Simulator_V3
{
    public class User
    {
        public static double numberOfUsers;

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        
        public User()
        {
        }

        public User(string name, string surname, string email)
        {
            Name = name;
            Surname = surname;
            Email = email;
        }
    }
}
