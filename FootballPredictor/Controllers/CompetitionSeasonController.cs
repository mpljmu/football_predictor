using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FootballPredictor.Models;

namespace FootballPredictor.Controllers
{
    public class CompetitionSeasonController : ApiController
    {

        // this needs to be moved to the players controller
        public IHttpActionResult GetPlayers(int id)
        {
            // 16 is the current season id
            var competitionSeason = new CompetitionSeason(id);
            var players = competitionSeason.GetPlayers();
            return Ok();
        }

    }
}
