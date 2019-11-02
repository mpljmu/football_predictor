using System.Collections.Generic;
using FootballPredictor.Models.Competitions;
using FootballPredictor.Models.Predictions;

namespace FootballPredictor.Models.People
{
    public interface IPlayer
    {
        int Id { get; set; }
        IUser User { get; set; }
        ICompetitionSeason CompetitionSeason { get; set; }
        IEnumerable<IPrediction> Predictions { get; set; }


        IEnumerable<IOpenPrediction> GetOpenPredictions();
        IEnumerable<IClosedPrediction> GetClosedPredictions();
        IEnumerable<IClosedPrediction> GetLiveClosedPredictions();
        IEnumerable<IClosedPrediction> GetCompletedClosedPredictions();
    }
}