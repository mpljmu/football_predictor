using FootballPredictor.Models.Connections;
using FootballPredictor.Models.Predictions;
using System.Collections.Generic;
using FootballPredictor.Models.Fixtures;
using FootballPredictor.Models.People;

namespace FootballPredictor.Models.Competitions
{
    public interface ICompetitionSeason
    {
        ICompetition Competition { get; }
        IEnumerable<IFixture> CompletedFixtures { get; }
        IEnumerable<IFixture> Fixtures { get; }
        IEnumerable<IFixture> FixturesOpenForPrediction { get; }
        int Id { get; set; }
        IEnumerable<IFixture> LiveFixtures { get; }
        IEnumerable<IPlayer> Players { get; }
        ISeason Season { get; }

        int CalculatePredictionPoints(IClosedPrediction prediction);
        int PointsFor(PredictionOutcome predictionOutcome);
    }
}