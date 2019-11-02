using FootballPredictor.Models.Competitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FootballPredictor.Controllers.CompetitionSeason
{
    public class CurrentCompetitionSeasonController : ApiController
    {
        private ICompetitionSeason CompetitionSeason { get; set; }

        public CurrentCompetitionSeasonController(ICompetitionSeason competitionSeason)
        {
            CompetitionSeason = competitionSeason;
        }

        public IHttpActionResult Get(int userId)
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}
