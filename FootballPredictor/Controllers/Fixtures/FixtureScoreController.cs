using FootballPredictor.Models.Connections;
using FootballPredictor.Models.Fixtures;
using FootballPredictor.Repositories.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FootballPredictor.Controllers.Fixtures
{
    public class FixtureScoreController : ApiController
    {
        private IFixtureRepository FixtureRepository { get; set; }


        public FixtureScoreController(IFixtureRepository fixtureRepository)
        {
            FixtureRepository = fixtureRepository;
        }


        public IHttpActionResult Put([FromUri] int fixtureId, [FromBody] FixtureScore score)
        {
            try
            {
                FixtureRepository.UpdateScore(fixtureId, score);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}
