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

        private static string _liveConnectionName = ConfigurationManager.ConnectionStrings["Live"].ConnectionString;
        private static SqlConnection _liveConnection = new SqlConnection(_liveConnectionName);
        private static SqlCommand _storedProcedure;

        public static SqlConnection LiveConnection
        {
            get
            {
                return _liveConnection;
            }
        }

    }
}