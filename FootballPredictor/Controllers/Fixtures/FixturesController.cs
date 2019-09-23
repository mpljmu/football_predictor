using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FootballPredictor.Models;
using FootballPredictor.Models.Competitions;
using FootballPredictor.Models.Fixtures;
using FootballPredictor.Repositories.Fixtures;

namespace FootballPredictor.Controllers.Fixtures
{
    public class FixturesController : ApiController
    {
        private IFixtureRepository FixtureRepository { get; set; }


        public FixturesController(IFixtureRepository fixtureRepository)
        {
            FixtureRepository = fixtureRepository;
        }


        public IHttpActionResult Get(int id)
        {
            try
            {
                var fixture = FixtureRepository.Get(id);
                return Ok(fixture);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        public IHttpActionResult Post(Fixture fixture )
        {
            try
            {
                FixtureRepository.Create(fixture);
                return Ok();
            }
            catch (Exception ex)
            {
                // TODO: Log
                return InternalServerError(ex);
            }
        }

    }
}
