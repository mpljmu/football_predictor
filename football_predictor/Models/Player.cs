using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace football_predictor.Models
{
    public class Player
    {
        private User user;
        private Competition competition;
        private Season season;
        public int Points {
            get
            {
                int totalPoints = 0;
                foreach (Prediction prediction in predictions)
                {
                    totalPoints += prediction.Points;
                }
                return totalPoints;
            }
        }

        private IEnumerable<Prediction> predictions;
        private Prediction[] form;

    }
}