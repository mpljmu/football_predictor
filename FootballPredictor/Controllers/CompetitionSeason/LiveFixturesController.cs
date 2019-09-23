using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FootballPredictor.Repositories.CompetitionSeason
{
    public class CompetitionSeasonLiveFixturesController : ApiController
    {
        private ICompetitionSeasonFixturesRepository CompetitionSeasonFixturesRepository { get; set; }


        public CompetitionSeasonLiveFixturesController(ICompetitionSeasonFixturesRepository competitionSeasonFixturesRepository)
        {
            CompetitionSeasonFixturesRepository = competitionSeasonFixturesRepository;
        }


        public IHttpActionResult Get(int competitionSeasonId)
        {
            try
            {
                var fixtures = CompetitionSeasonFixturesRepository.Get(competitionSeasonId).ToList();
                // Remove fixtures which aren't live - live is defined by closed for predictions but not completed
                for (int i = fixtures.Count() - 1; i >= 0; i--)
                {
                    if (fixtures[i].Completed || fixtures[i].OpenForPredictions)
                    {
                        fixtures.RemoveAt(i);
                    }
                }
                return Ok(fixtures);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
