using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace FootballPredictor.Models.Connections
{
    public class DatabaseConnection : Connection, IDatabaseConnection
    {

        public IDbConnection Connection { get; protected set; }
        public IDbCommand Command { get; protected set; }

    }
}