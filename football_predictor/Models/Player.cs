using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPredictor.Models
{
    public class Player
    {
        private User _user;
        private Competition _competition;
        private Season _season;
        public int Points {
            get
            {
                int totalPoints = 0;
                foreach (Prediction prediction in _predictions)
                {
                    totalPoints += prediction.Points;
                }
                return totalPoints;
            }
        }

        private IEnumerable<Prediction> _predictions;
        private Prediction[] _form;

    }
}