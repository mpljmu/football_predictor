using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace football_predictor.Models
{
    public class Fixture
    {
        private enum Points
        {
            CorrectScore = 3,
            CorrectOutcome = 1,
            Incorrect = 0
        }

        private int id;
        private Club homeClub;
        private Club awayClub;
        private DateTime date;
        private int? homeGoals;
        private int? awayGoals;
        public string Score {
            get
            {
                if (homeGoals == null || awayGoals == null)
                {
                    return string.Format("{0}-{1}", homeGoals, awayGoals);
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// Calculate the amount of points generated for a prediction
        /// </summary>
        /// <param name="homeGoals"></param>
        /// <param name="awayGoals"></param>
        /// <returns></returns>
        public int CalculatePredictionPoints(int homeGoals, int awayGoals)
        {
            if (homeGoals == this.homeGoals && awayGoals == this.awayGoals)
            {
                return (int)Points.CorrectScore;
            }
            else if (homeGoals == awayGoals && this.homeGoals == this.awayGoals)
            {
                return (int)Points.CorrectOutcome;
            }
            else if (
                (homeGoals > awayGoals && this.homeGoals > this.awayGoals)
                || (awayGoals < homeGoals && this.awayGoals < this.homeGoals)
            )
            {
                return (int)Points.CorrectOutcome;
            }
            else
            {
                return (int)Points.Incorrect;
            }
        }

        public static IEnumerable<Fixture> GetAllFixtures(string season, string competition)
        {
            
            return null;
        }


    }
}