using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Configuration;

namespace football_predictor.Models
{
    public class Connection
    {

        private static string _databaseConnection = ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString;

        public static SqlConnection DatabaseConnection
        {
            get
            {
                return new SqlConnection(_databaseConnection);
            }
        }
    }
}