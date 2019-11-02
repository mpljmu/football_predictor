using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FootballPredictor.Models;
using FootballPredictor.Repositories.Competitions;

namespace FootballPredictor.Controllers.Comeptitions
{
    public class CompetitionsController : ApiController
    {
        private ICompetitionRepository CompetitionRepository { get; set; }

        public CompetitionsController(ICompetitionRepository competitionRepository)
        {
            CompetitionRepository = competitionRepository;
        }

        public IHttpActionResult Get(int userId)
        {
            try
            {
                var competitions = CompetitionRepository.GetByUserId(userId);
                return Ok(competitions);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
