using System.Collections.Generic;
using FootballPredictor.Models.People;
using FootballPredictor.Models.Predictions;

namespace FootballPredictor.Models.Competitions
{
    public interface IPlayer
    {
        int Id { get; }
        ICompetitionSeason CompetitionSeason { get; }
        IEnumerable<OpenPrediction> OpenPredictions { get; }
        IEnumerable<ClosedPrediction> ClosedPredictions { get; }
        IUser User { get; }
    }
}