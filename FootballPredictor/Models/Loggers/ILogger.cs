using System;

namespace FootballPredictor.Models.Loggers
{
    public interface ILogger
    {
        void Insert(string message, IDatabaseLoggerCategory classification, DateTime dateTime, int userId);
        void Insert(string message, string tableName, int recordId, IDatabaseLoggerCategory classification, DateTime dateTime, int userId);
        void Insert(string message, string className, string methodName, IDatabaseLoggerCategory classification, DateTime dateTime, int userId);
    }
}