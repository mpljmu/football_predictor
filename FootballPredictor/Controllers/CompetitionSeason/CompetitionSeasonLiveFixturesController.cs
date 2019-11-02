using FootballPredictor.Repositories.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FootballPredictor.Controllers.CompetitionSeason
{
    public class CompetitionSeasonLiveFixturesController : ApiController
    {
        IFixturesRepository FixturesRepository { get; set; }


        public CompetitionSeasonLiveFixturesController(IFixturesRepository fixturesRepository)
        {
            FixturesRepository = fixturesRepository;
        }


        public IHttpActionResult Get(int competitionSeasonId)
        {
            try
            {
                var fixtures = FixturesRepository.GetLiveByCompetitionSeason(competitionSeasonId);
                return Ok(fixtures);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
