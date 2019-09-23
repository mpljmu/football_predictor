using FootballPredictor.Repositories.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FootballPredictor.Controllers.Fixtures
{
    public class FixtureDateController : ApiController
    {
        private IFixtureRepository FixtureRepository { get; set; }

        
        public FixtureDateController(IFixtureRepository fixtureRepository)
        {
            FixtureRepository = fixtureRepository;
        }


        public IHttpActionResult Get(int fixtureId)
        {
            try
            {
                var fixture = FixtureRepository.Get(fixtureId);
                return Ok(fixture.Date);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        public IHttpActionResult Post(int fixtureId, DateTime date)
        {
            try
            {
                FixtureRepository.SetDate(fixtureId, date);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
