using FootballPredictor.Models.Predictions;
using FootballPredictor.Models.Rankings;
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
    public class CompetitionSeasonCompletedRankingsController : ApiController
    {
        private IPlayerRepository PlayerRepository { get; set; }
        private IPredictionRepository PredictionRepository { get; set; }
        private ICompetitionSeasonRepository CompetitionSeasonRepository { get; set; }


        public CompetitionSeasonCompletedRankingsController(IPlayerRepository playerRepository, IPredictionRepository predictionRepository, ICompetitionSeasonRepository competitionSeasonRepository)
        {
            PlayerRepository = playerRepository;
            PredictionRepository = predictionRepository;
            CompetitionSeasonRepository = competitionSeasonRepository;
        }


        public IHttpActionResult Get(int competitionSeasonId)
        {
            try
            {
                // Get the competition season object to work with
                var competitionSeason = CompetitionSeasonRepository.Get(competitionSeasonId);
                // Get all of the players from the competition season
                var players = PlayerRepository.GetAllByCompetitionSeason(competitionSeasonId);
                // For each of the players get the completed fixture closed predictions and create a ranking object
                var rankings = new List<IRanking>();
                foreach (var player in players)
                {
                    player.Predictions = PredictionRepository.GetByCompetitionSeasonPlayer(competitionSeasonId, player.Id);
                    var completedClosedPredictions = player.GetCompletedClosedPredictions();
                    var ranking = new Ranking(player.User.FullName, completedClosedPredictions, competitionSeason);
                    rankings.Add(ranking);
                }
                Ranking.OrderRankings(rankings);
                return Ok(rankings);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
