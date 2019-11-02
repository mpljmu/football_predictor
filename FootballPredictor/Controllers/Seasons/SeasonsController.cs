using FootballPredictor.Repositories.Seasons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FootballPredictor.Controllers.Seasons
{
    public class SeasonsController : ApiController
    {
        private ISeasonRepository SeasonRepository { get; set; }

        public SeasonsController(ISeasonRepository seasonRepository)
        {
            SeasonRepository = seasonRepository;
        }


        public IHttpActionResult Get(int competitionId)
        {
            try
            {
                var seasons = SeasonRepository.GetByCompetitionId(competitionId);
                return Ok(seasons);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
