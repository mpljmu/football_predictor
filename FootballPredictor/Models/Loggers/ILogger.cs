using System;

namespace FootballPredictor.Models.Loggers
{
    public interface ILogger
    {
        void Insert(string message, DatabaseLoggerCategory classification, DateTime dateTime, int userId);
        void Insert(string message, string tableName, int recordId, DatabaseLoggerCategory classification, DateTime dateTime, int userId);
        void Insert(string message, string className, string methodName, DatabaseLoggerCategory classification, DateTime dateTime, int userId);
    }
}