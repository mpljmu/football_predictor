using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace football_predictor.Models
{
    public class Fixture
    {
        public int Id { get; set; }
        public Club HomeClub { get; set; }
        public Club AwayClub { get; set; }
        public DateTime Date { get; set; }
        public int? HomeGoals { get; set; }
        public int? AwayGoals { get; set; }
        public string Score {
            get
            {
                if (HomeGoals == null || AwayGoals == null)
                {
                    return string.Format("{0}-{1}", HomeGoals, AwayGoals);
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public static IEnumerable<Fixture> GetAllFixtures(string season, string competition)
        {
            
            return null;
        }


    }
}