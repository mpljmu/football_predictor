using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FootballPredictor.Models;

namespace FootballPredictor.Controllers
{
    public class FixturesController : ApiController
    {

        public IHttpActionResult Put(Fixture fixture)
        {
            // update the fixture in the database
            try
            {
                fixture.UpdateFixture();
            }
            catch(Exception ex)
            {
                //return InternalServerErrorResult();
            }
          
            return Ok();
        }

        public IHttpActionResult GetFixtures(int competitionId) // also needs the season
        {
            var competitionSeason = new CompetitionSeason(competitionId);
            var fixtures = competitionSeason.GetFixtures();
            return Ok();
        }


    }
}
