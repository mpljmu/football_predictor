using FootballPredictor.Models.Competitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPredictor.Models.Fixtures
{
    public class FixtureScore : IFixtureScore
    {
        public byte HomeGoals { get; protected set; }
        public byte AwayGoals { get; protected set; }
        public string EnteredScore
        {
            get
            {
                return string.Format("{0}-{1}", HomeGoals, AwayGoals);
            }
        }


        public FixtureScore(byte homeGoals, byte awayGoals)
        {
            HomeGoals = homeGoals;
            AwayGoals = awayGoals;
        }

    }
}