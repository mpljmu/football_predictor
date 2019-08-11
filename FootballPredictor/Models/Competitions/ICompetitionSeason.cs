using System.Collections.Generic;
using FootballPredictor.Models.Connections;
using FootballPredictor.Models.Predictions;

namespace FootballPredictor.Models.Competitions
{
    public interface ICompetitionSeason
    {
        ICompetition Competition { get; }
        IEnumerable<IFixture> CompletedFixtures { get; }
        IDatabaseConnection DatabaseConnection { set; }
        IEnumerable<IFixture> Fixtures { get; }
        IEnumerable<IFixture> FixturesOpenForPrediction { get; }
        int Id { get; set; }
        IEnumerable<IFixture> LiveFixtures { get; }
        IEnumerable<IPlayer> Players { get; }
        ISeason Season { get; }

        void AddPlayer(int playerId);
        int CalculatePredictionPoints(Prediction prediction);
        void GetFixtures();
        void GetPlayers();
        int PointsFor(PredictionOutcome predictionOutcome);
    }
}