using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace lume.Models
{
    public class User
    {
        [PrimaryKey]
        public int Id { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }

        public User() { }
        public User(string Username, string Password) 
        {
            this.Username = Username ?? "";
            this.Password = Password ?? "";
        }

        public bool CheckInformation() 
        {
            if (!Username.Equals("") && !Password.Equals(""))
                return true;
            else
                return false;
        }

    }
}
