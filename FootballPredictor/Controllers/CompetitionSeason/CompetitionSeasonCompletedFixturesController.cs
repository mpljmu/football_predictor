using FootballPredictor.Repositories.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FootballPredictor.Repositories.CompetitionSeason
{
    public class CompetitionSeasonCompletedFixturesController : ApiController
    {
        private IFixturesRepository FixturesRepository { get; set; }


        public CompetitionSeasonCompletedFixturesController(IFixturesRepository fixturesRepository)
        {
            FixturesRepository = fixturesRepository;
        }


        public IHttpActionResult Get(int competitionSeasonId)
        {
            try
            {
                var fixtures = FixturesRepository.GetCompletedByCompetitionSeason(competitionSeasonId);
                return Ok(fixtures);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}
