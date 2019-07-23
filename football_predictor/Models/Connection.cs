using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace FootballPredictor.Models
{
    public class Connection
    {

    }

    public class DatabaseConnection : Connection
    {
        /*
         * Use these variables to indicate which database implementation the application will use i.e. SqlConnection for SqlServer
         */
        private SqlConnection _databaseConnection = new SqlConnection();
        private SqlCommand _databaseCommand = new SqlCommand();

        private IDbConnection _connection;
        public IDbConnection Connection {
            get
            {
                return _connection;
            }
            private set
            {
                value.ConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString;
                _connection = value;
            }
        }
        public IDbCommand Command { get; }
        public DatabaseConnection()
        {
            Connection = _databaseConnection;
        }

        public DatabaseConnection(string commandText)
        {
            Connection = _databaseConnection;
            _databaseCommand.CommandText = commandText;
            Command = _databaseCommand;
        }


    }

}