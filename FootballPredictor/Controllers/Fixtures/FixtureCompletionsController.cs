using FootballPredictor.Repositories.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FootballPredictor.Controllers.Fixtures
{
    public class FixtureCompletionsController : ApiController
    {
        private IFixtureRepository FixtureRepository { get; set; }
        

        public FixtureCompletionsController(IFixtureRepository fixtureRepository)
        {
            FixtureRepository = fixtureRepository;
        }

        public IHttpActionResult Get(int fixtureId)
        {
            try
            {
                var completed = FixtureRepository.IsCompleted(fixtureId);
                return Ok(completed);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }
        public IHttpActionResult Post(int fixtureId)
        {
            try
            {
                FixtureRepository.Complete(fixtureId);
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
