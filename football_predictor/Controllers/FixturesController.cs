﻿using System;
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
        public IEnumerable<Fixture> GetAllFixtures(string competition, string season)
        {
            var fixtures = Models.Fixture.GetAllFixtures(competition, season);
            
            return Ok(fixtures);
        }

    }
}
