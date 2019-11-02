using FootballPredictor.Models.People;
using FootballPredictor.Models.Predictions;
using FootballPredictor.Repositories.CompetitionSeason;
using FootballPredictor.Repositories.Predictions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FootballPredictor.Controllers.CompetitionSeason
{
    public class CompetitionSeasonPredictionsController : ApiController
    {
        private IPredictionRepository PredictionRepository { get; set; }
        private IPlayerRepository PlayerRepository { get; set; }


        public CompetitionSeasonPredictionsController(IPredictionRepository predictionRepository, IPlayerRepository playerRepository)
        {
            PredictionRepository = predictionRepository;
            PlayerRepository = playerRepository;
        }


        public  IHttpActionResult Get(int competitionSeasonId, int playerId)
        {
            try
            {
                var player = PlayerRepository.Get(playerId);
                var predictions = PredictionRepository.GetByCompetitionSeasonPlayer(competitionSeasonId, playerId);
                player.Predictions = predictions;
                return Ok(player);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
