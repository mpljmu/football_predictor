using FootballPredictor.Repositories.CompetitionSeason;
using FootballPredictor.Repositories.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FootballPredictor.Controllers.CompetitionSeason
{
    public class FixturesController : ApiController
    {
        private ICompetitionSeasonFixturesRepository CompetitionSeasonFixturesRepository { get; set; }


        public FixturesController(ICompetitionSeasonFixturesRepository competitionSeasonFixturesRepository)
        {
            CompetitionSeasonFixturesRepository = competitionSeasonFixturesRepository;
        }


        public IHttpActionResult Get(int competitionSeasonId)
        {
            try
            {
                var fixtures = CompetitionSeasonFixturesRepository.Get(competitionSeasonId);
                return Ok(fixtures);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
