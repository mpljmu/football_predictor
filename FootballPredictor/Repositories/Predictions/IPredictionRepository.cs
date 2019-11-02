using FootballPredictor.Models.Predictions;
using System.Collections.Generic;

namespace FootballPredictor.Repositories.Predictions
{
    public interface IPredictionRepository
    {
        IEnumerable<IPrediction> GetByCompetitionSeasonFixture(int competitionSeasonId, int fixtureId);
        IEnumerable<IPrediction> GetByCompetitionSeasonPlayer(int competitionSeasonId, int playerId);
    }
}