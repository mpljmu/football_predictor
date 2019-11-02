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
    public class CompetitionSeasonPlayerOpenPredictionsController : ApiController
    {
        private IPlayerRepository PlayerRepository { get; set; }
        private IPredictionRepository PredictionRepository { get; set; }


        public CompetitionSeasonPlayerOpenPredictionsController(IPlayerRepository playerRepository, IPredictionRepository predictionRepository)
        {
            PlayerRepository = playerRepository;
            PredictionRepository = predictionRepository;
        }


        public IHttpActionResult Get(int competitionSeasonId, int playerId)
        {
            try
            {
                var player = PlayerRepository.Get(playerId);
                player.Predictions = PredictionRepository.GetByCompetitionSeasonPlayer(competitionSeasonId, playerId);
                var closedPredictions = player.GetOpenPredictions();
                return Ok(closedPredictions);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
