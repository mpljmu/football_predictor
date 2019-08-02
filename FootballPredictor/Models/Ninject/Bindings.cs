using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using Ninject.Modules;
using FootballPredictor.Models;

namespace FootballPredictor.Models.Ninject
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<Connections.IDatabaseConnection>().To<Connections.SqlServerDatabaseConnection>();
        }
    }
}