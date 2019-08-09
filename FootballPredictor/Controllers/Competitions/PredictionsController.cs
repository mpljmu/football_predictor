using FootballPredictor.Models.Competitions;
using FootballPredictor.Models.Connections;
using FootballPredictor.Models.Loggers;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;

namespace FootballPredictor.Controllers.Competitions
{
    public class PredictionsController : ApiController
    {
        private IDatabaseConnection DatabaseConnection { get; set; }
        private ILogger Logger { get; set; }
        private IPlayer Player { get; set; }


        public PredictionsController(IDatabaseConnection databaseConnection, ILogger logger, IPlayer player)
        {
            DatabaseConnection = databaseConnection;
            Logger = logger;
        }

        public IEnumerable<string> Get(string competitionId, string season)
        {
            return new List<string>();
        }
        public Prediction Get(int id)
        {
            var prediction = new Prediction(id, DatabaseConnection, Logger);
            prediction.PopulateObject();
            return prediction;
        }
        public void Post([FromBody]Prediction prediction)
        {

        }
        public IHttpActionResult PutScore(int id, [FromBody]int homeGoals, int awayGoals)
        {
            try
            {
                var prediction = new Prediction(id, DatabaseConnection, Logger);
                prediction.UpdateScore(homeGoals, awayGoals, );
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError();
                // TODO
            }
        }
        public void Delete(int id)
        {
        }
    }
}
