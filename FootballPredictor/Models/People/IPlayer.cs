using System.Collections.Generic;
using FootballPredictor.Models.Competitions;
using FootballPredictor.Models.Predictions;

namespace FootballPredictor.Models.People
{
    public interface IPlayer
    {
        IEnumerable<IPrediction> ClosedPredictions { get; }
        ICompetitionSeason CompetitionSeason { get; set; }
        int CompletedCorrectOutcomes { get; }
        int CompletedCorrectScores { get; }
        int CompletedIncorrectOutcomes { get; }
        int CompletedTotalPoints { get; }
        int CompletedTotalPredictions { get; }
        int CompletedMissedPredictions { get; }
        int Id { get; set; }
        IEnumerable<IPrediction> LiveClosedPrediction { get; }
        IEnumerable<IOpenPrediction> OpenPredictions { get; }
        IEnumerable<IPrediction> Predictions { get; set; }
        IUser User { get; set; }
    }
}