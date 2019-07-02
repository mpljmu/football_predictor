using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace football_predictor.Models
{
    public class Prediction
    {
        private int id;
        private User user;
        private Fixture fixture;
        private int? homeGoals;
        private int? awayGoals;

        public int Points
        {
            get
            {
                return fixture.CalculatePredictionPoints((int)homeGoals, (int)awayGoals);
            }
        }

    }
}