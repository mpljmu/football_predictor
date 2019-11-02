using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPredictor.Models.Security
{
    public class UserLogin : IUserLogin
    {
        public static string FailedLoginMessage
        {
            get
            {
                return "Username or password incorrect";
            }
        }

        public string username { get; private set; }
        public string password { get; private set; }


        public UserLogin(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

    }
}