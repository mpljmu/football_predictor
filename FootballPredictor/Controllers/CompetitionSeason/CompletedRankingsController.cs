using FootballPredictor.Models.Rankings;
using FootballPredictor.Repositories.CompetitionSeason;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FootballPredictor.Controllers.CompetitionSeason
{
    public class CompletedRankingsController : ApiController
    {
        private  ICompetitionSeasonPlayersRepository CompetitionSeasonPlayersRepository { get; set; }


        public CompletedRankingsController(ICompetitionSeasonPlayersRepository competitionSeasonPlayersRepository)
        {
            CompetitionSeasonPlayersRepository = competitionSeasonPlayersRepository;
        }


        public IHttpActionResult Get(int competitionSeasonId)
        {
            try
            {
                // Get all of the players from the competition season
                var players = CompetitionSeasonPlayersRepository.Get(competitionSeasonId);
                // For each of the players get the predictions
                foreach (var player in players)
                {
                    player.Predictions = CompetitionSeasonPlayersRepository.GetPredictions(player.CompetitionSeason.Id, player.Id);
                }
                var rankings = Ranking.RankingsForCompletedClosedFixtures(players);
                return Ok(rankings);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
