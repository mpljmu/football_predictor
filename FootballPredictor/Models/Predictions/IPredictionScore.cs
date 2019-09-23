using FootballPredictor.Models.Competitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballPredictor.Models.Predictions
{
    public interface IPredictionScore
    {
        byte HomeGoals { get; }
        byte AwayGoals { get; }
    }
}
