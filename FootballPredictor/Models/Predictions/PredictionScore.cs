using FootballPredictor.Models.Competitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPredictor.Models.Predictions
{
    public class PredictionScore : Score
    {
        public PredictionScore(byte homeGoals, byte awayGoals)
            : base(homeGoals, awayGoals)
        {

        }
    }
}