using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FootballPredictor.Models.People;

namespace FootballPredictor.Models.Competitions
{
    public class Player
    {
        public User User { get; private set; }
        public Competition Competition { get; private set; }
        public Season Season { get; private set; }
        public int Points {
            get
            {
                int totalPoints = 0;
                foreach (Prediction prediction in Predictions)
                {
                    totalPoints += prediction.Points;
                }
                return totalPoints;
            }
        }
        public IEnumerable<Prediction> Predictions { get; private set; }
        private Prediction[] Form { get; }



    }
}