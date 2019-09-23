using FootballPredictor.Models.Competitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPredictor.Models.Predictions
{
    public class PredictionScore : IPredictionScore
    {
        public byte HomeGoals { get; private set; }
        public byte AwayGoals { get; private set; }


        public PredictionScore(byte homeGoals, byte awayGoals)
        {
            HomeGoals = homeGoals;
            AwayGoals = awayGoals;
        }
    }
}