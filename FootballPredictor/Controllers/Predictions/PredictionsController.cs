using FootballPredictor.Models.Competitions;
using FootballPredictor.Models.Connections;
using FootballPredictor.Models.Loggers;
using FootballPredictor.Models.Predictions;
using FootballPredictor.Repositories.Predictions;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;

namespace FootballPredictor.Controllers.Predictions
{
    public class PredictionsController : ApiController
    {
        private ILogger Logger { get; set; }
        private IPrediction Prediction { get; set; }
        private IOpenPredictionRepository OpenPredictionRepository { get; set; }


        public PredictionsController(IDatabaseConnection databaseConnection, ILogger logger, IPrediction prediction, IOpenPredictionRepository openPredictionRepository)
        {
            Logger = logger;
            Prediction = prediction;
            OpenPredictionRepository = openPredictionRepository;
        }


        public IHttpActionResult Get(int id)
        {
            try
            {
                var prediction = OpenPredictionRepository.Get(id);
                return Ok(prediction);
            }
            catch (Exception ex)
            {
                // TODO: Log
                return InternalServerError(ex);
            }
        }
        public IHttpActionResult Post([FromBody]OpenPrediction prediction)
        {
            try
            {
                OpenPredictionRepository.Insert(prediction);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        public IHttpActionResult Put([FromBody]int id, PredictionScore score)
        {
            if (score == null)
            {
                return InternalServerError(new Exception("Pediction score not valid"));
            }
            try
            {
                // prediction.Player = get the player from the token authentication module
                OpenPredictionRepository.UpdateScore(id, score);
                return Ok();
            }
            catch (Exception ex)
            {
                // TODO: Log
                return InternalServerError(ex);
            }
        }
        public IHttpActionResult Delete(int id)
        {
            try
            {
                OpenPredictionRepository.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
