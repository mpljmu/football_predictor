using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using FootballPredictor.Models.People;

namespace FootballPredictor.Models.People
{
    public class User : Person
    {
        private int Id { get; set; }
        private string Username { get; set; }
        private string FullName { get
            {
                return string.Format("{0} {1}", Forename, Surname);
            }
        }
        private string Hash { get; set; }
        private bool Active { get; set; }
        private bool IsAdministrator {get; set;}


        public User(int id, string username, string surname, string forename)
        {
            Id = id;
            Username = username;
            Surname = surname;
            Forename = forename;
        }


        public void Create()
        {

        }

    }
}