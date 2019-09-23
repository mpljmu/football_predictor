using FootballPredictor.Models.Competitions;
using FootballPredictor.Models.People;
using FootballPredictor.Repositories.CompetitionSeason;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FootballPredictor.Controllers.CompetitionSeason
{
    public class PlayersController : ApiController
    {
        private ICompetitionSeasonPlayersRepository CompetitionSeasonPlayerRepository { get; set; }


        public PlayersController(ICompetitionSeasonPlayersRepository competitionSeasonPlayerRepository)
        {
            CompetitionSeasonPlayerRepository = competitionSeasonPlayerRepository;

        }


        public IHttpActionResult Get(int competitionSeasonId)
        {
            try
            {
                var players = CompetitionSeasonPlayerRepository.Get(competitionSeasonId);
                return Ok(players);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        public IHttpActionResult Post([FromUri] int competitionSeasonId, [FromBody] IPlayer player)
        {
            try
            {
                CompetitionSeasonPlayerRepository.Add(competitionSeasonId, player);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}
