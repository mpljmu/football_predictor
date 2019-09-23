using FootballPredictor.Repositories.Fixtures;
using FootballPredictor.Repositories.Predictions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FootballPredictor.Controllers.CompetitionSeason
{
    public class CompetitionSeasonCompletedFixturePredictionsController : ApiController
    {
        private IPredictionRepository PredictionRepository { get; set; }
        private IFixtureRepository FixtureRepository { get; set; }

        public CompetitionSeasonCompletedFixturePredictionsController(IPredictionRepository predictionRepository, IFixtureRepository fixtureRepository)
        {
            PredictionRepository = predictionRepository;
            FixtureRepository = fixtureRepository;
        }

        public IHttpActionResult Get(int competitionSeasonId, int fixtureId)
        {
            try
            {
                // Only want to return the predictions if the fixture is completed for this controller
                var fixture = FixtureRepository.Get(fixtureId);
                if (fixture == null)
                {
                    return BadRequest("The fixture does not exist");
                }
                if (!fixture.Completed)
                {
                    return BadRequest("The fixture has not been completed");
                } else
                {
                    var predictions = PredictionRepository.GetByCompetitionSeasonFixture(competitionSeasonId, fixtureId);
                    return Ok(predictions);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
