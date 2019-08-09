using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPredictor.Models.Loggers
{
    public class DatabaseLoggerCategory
    {
        public string Name { get; private set;}


        public static DatabaseLoggerCategory Success
        {
            get
            {
                return new DatabaseLoggerCategory("Success");
            }
        }
        public static DatabaseLoggerCategory Warning
        {
            get
            {
                return new DatabaseLoggerCategory("Warning");
            }
        }
        public static DatabaseLoggerCategory Error
        {
            get
            {
                return new DatabaseLoggerCategory("Error");
            }
        }


        public DatabaseLoggerCategory(string name)
        {
            Name = name;
        }
        

    }
}