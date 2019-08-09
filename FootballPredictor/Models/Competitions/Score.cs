﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPredictor.Models.Competitions
{
    public abstract class Score
    {
        public int HomeGoals { get; private set; }
        public int AwayGoals { get; private set; }
        public string EnteredScore
        {
            get
            {
                return string.Format("{0}-{1}", HomeGoals, AwayGoals);
            }
        }


        public Score(byte homeGoals, byte awayGoals)
        {
            HomeGoals = homeGoals;
            AwayGoals = awayGoals;
        }
    }
}