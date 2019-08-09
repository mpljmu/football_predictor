using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FootballPredictor.Models.Connections;
using FootballPredictor.Models.People;

namespace FootballPredictor.Models.Loggers
{
    public class DatabaseLogger : Logger
    {
        private IDatabaseConnection DatabaseConnection { get; set; }


        public DatabaseLogger(IDatabaseConnection databaseConnection)
        {
            DatabaseConnection = databaseConnection;
        }


        public override void Insert(string message, Category classification, DateTime dateTime, int userId)
        {

        }
        public override void Insert(string message, string tableName, int recordId, Category classification, DateTime dateTime, int userId)
        {

        }
    }
}