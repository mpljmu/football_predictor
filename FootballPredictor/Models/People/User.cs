using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using FootballPredictor.Models.Connections;
using FootballPredictor.Models.People;
using Ninject;
using FootballPredictor.App_Start;

namespace FootballPredictor.Models.People
{
    public class User : Person, IUser
    {
        public int Id { get; private set; }
        public string Username { get; private set; }
        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", Forename, Surname);
            }
        }
        public IPassword Password { private get; set; }
        public bool Active { get; set; }
        public bool IsAdministrator { get; set; }
        [Inject]
        public IDatabaseConnection databaseConnection { private get; set; }


        public User(int id, string username, string surname, string forename)
        {
            Id = id;
            Username = username;
            Surname = surname;
            Forename = forename;
            NinjectWebCommon.Bootstrapper.Kernel.Inject(this);
        }


        public void Create()
        {

        }
        public void UpdatePassword(IPassword password)
        {

        }
        public void UpdateActive(bool active)
        {

        }
    }
}