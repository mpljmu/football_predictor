using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FootballPredictor.Models.People;

namespace FootballPredictor.Models.Loggers
{
    public abstract class Logger : ILogger
    {


        public abstract void Insert(string message, IDatabaseLoggerCategory classification, DateTime dateTime, int userId);
        public abstract void Insert(string message, string tableName, int recordId, IDatabaseLoggerCategory classification, DateTime dateTime, int userId);
        public abstract void Insert(string message, string className, string methodName, IDatabaseLoggerCategory classification, DateTime dateTime, int userId);

    }
}