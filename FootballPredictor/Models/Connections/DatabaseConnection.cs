using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace FootballPredictor.Models.Connections
{
    public abstract class DatabaseConnection : Connection, IDatabaseConnection
    {

        public abstract IDbConnection NewConnection();


    }
}