using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FootballPredictor.Models.Connections
{
    public class SqlServerDatabaseConnection : DatabaseConnection
    {
        private string ConnectionString { get; }


        public SqlServerDatabaseConnection()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString;
        }
        
        public override IDbConnection NewConnection()
        {
            return new SqlConnection(ConnectionString);
        }

    }
}