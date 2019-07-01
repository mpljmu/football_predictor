using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace football_predictor.Models
{
    public class Player
    {
        public User user { get; set; }
        public Competition competition { get; set; }
        public Season season { get; set; }

        /// <summary>
        /// Total number of points for the season
        /// </summary>
        public int points {
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

        public IEnumerable<Prediction> Predictions { get; set; }
        public int[] Form { get; set; }

    }
}