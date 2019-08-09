using System.Collections.Generic;
using FootballPredictor.Models.People;

namespace FootballPredictor.Models.Competitions
{
    public interface IPlayer
    {
        ICompetition Competition { get; }
        IEnumerable<IPrediction> Predictions { get; }
        ISeason Season { get; }
        IUser User { get; }
    }
}