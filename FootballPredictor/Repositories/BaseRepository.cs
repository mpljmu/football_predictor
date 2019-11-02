using FootballPredictor.Models.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPredictor.Repositories
{
    public class BaseRepository
    {
        protected IDatabaseConnection DatabaseConnection { get; set; }


        public BaseRepository(IDatabaseConnection databaseConnection)
        {
            DatabaseConnection = databaseConnection;
        }
    }
}