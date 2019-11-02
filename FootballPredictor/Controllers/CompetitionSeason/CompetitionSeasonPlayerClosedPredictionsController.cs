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
    public class CompetitionSeasonPlayerClosedPredictionsController : ApiController
    {
        private IPlayerRepository PlayerRepository { get; set; }
        private IPredictionRepository PredictionRepository { get; set; }
        private ICompetitionSeasonRepository CompetitionSeasonRepository { get; set; }


        public CompetitionSeasonPlayerClosedPredictionsController(IPlayerRepository playerRepository, IPredictionRepository predictionRepository, ICompetitionSeasonRepository competitionSeasonRepository)
        {
            PlayerRepository = playerRepository;
            PredictionRepository = predictionRepository;
            CompetitionSeasonRepository = competitionSeasonRepository;
        }


        public IHttpActionResult Get(int competitionSeasonId, int playerId)
        {
            try
            {
                var player = PlayerRepository.Get(playerId);
                player.Predictions = PredictionRepository.GetByCompetitionSeasonPlayer(competitionSeasonId, playerId);
                var closedPredictions = player.GetClosedPredictions();
                var competitionSeason = CompetitionSeasonRepository.Get(competitionSeasonId);
                return Ok(closedPredictions);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
