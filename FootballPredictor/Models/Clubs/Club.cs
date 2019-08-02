using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FootballPredictor.Models.Connections;

namespace FootballPredictor.Models.Clubs
{
    public class Club
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public IDatabaseConnection DatabaseConnection { get; set; }


        public Club(int id, string name, IDatabaseConnection databaseConnection)
        {
            Id = id;
            Name = name;
            DatabaseConnection = databaseConnection;
        }

    }
}