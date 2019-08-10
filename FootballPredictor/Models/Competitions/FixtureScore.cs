using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPredictor.Models.Competitions
{
    public class FixtureScore : Score
    {


        public FixtureScore(byte homeGoals, byte awayGoals)
            : base(homeGoals, awayGoals)
        {

        }
    }
}