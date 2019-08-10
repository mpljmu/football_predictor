using FootballPredictor.Models.Predictions;
using System.Collections.Generic;

namespace FootballPredictor.Models.Competitions
{
    public interface ICompetitionSeason
    {
        ICompetition Competition { get; }
        int Id { get; set; }
        ISeason Season { get; }
        void AddPlayer(int playerId);
        IEnumerable<Fixture> GetFixtures();
        IEnumerable<Player> GetPlayers();


        int PointsFor(PredictionOutcome predictionOutcome);
        int CalculatePredictionPoints(ClosedPrediction prediction);
    }
}