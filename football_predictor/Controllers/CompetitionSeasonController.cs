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
        public IEnumerable<Fixture> GetFixtures(int id)
        {
            var competitionSeason = new CompetitionSeason(id);
            var fixtures = competitionSeason.GetFixtures();
            return fixtures;
        }

    }
}
