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


        public override void Insert(string message, IDatabaseLoggerCategory classification, DateTime dateTime, int userId)
        {
            throw new NotImplementedException();
        }
        public override void Insert(string message, string tableName, int recordId, IDatabaseLoggerCategory classification, DateTime dateTime, int userId)
        {
            throw new NotImplementedException();
        }
        public override void Insert(string message, string className, string methodName, IDatabaseLoggerCategory classification, DateTime dateTime, int userId)
        {
            throw new NotImplementedException();
        }
    }
}