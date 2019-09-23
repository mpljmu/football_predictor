[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(FootballPredictor.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(FootballPredictor.App_Start.NinjectWebCommon), "Stop")]

namespace FootballPredictor.App_Start
{
    using System;
    using System.Web;
    using FootballPredictor.Controllers.CompetitionSeason;
    using FootballPredictor.Models.Competitions;
    using FootballPredictor.Models.Connections;
    using FootballPredictor.Models.Loggers;
    using FootballPredictor.Models.People;
    using FootballPredictor.Repositories.CompetitionSeason;
    using FootballPredictor.Repositories.Fixtures;
    using FootballPredictor.Repositories.Predictions;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;

    public static class NinjectWebCommon 
    {
        public static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            Bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IDatabaseConnection>().To<SqlServerDatabaseConnection>();
            kernel.Bind<ILogger>().To<DatabaseLogger>();
            kernel.Bind<IPlayer>().To<Player>();
            kernel.Bind<IFixtureRepository>().To<FixtureRepository>();
            kernel.Bind<ICompetitionSeasonPlayersRepository>().To<CompetitionSeasonPlayersRepository>();
            kernel.Bind<ICompetitionSeasonFixturesRepository>().To<CompetitionSeasonFixturesRepository>();
            kernel.Bind<IPredictionRepository>().To<PredictionRepository>();
        }        
    }
}