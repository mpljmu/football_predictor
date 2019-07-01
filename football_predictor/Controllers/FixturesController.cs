using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using football_predictor.Models;

namespace football_predictor.Controllers
{
    public class FixturesController : ApiController
    {
        public IEnumerable<Fixture> GetAllFixtures()
        {
            //FILTER: se query string to identify competition and season


            //Fixture.GetAllFixtures
            //return Ok()
            return null;
        }

    }
}
