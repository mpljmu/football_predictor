using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace football_predictor.Models
{
    public class Prediction
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Fixture Fixture { get; set; }
        public int? HomeGoals { get; set; }
        public int? AwayGoals { get; set; }

        /// <summary>
        /// Compute the number of points based on the Prediction, and the Fixture score
        /// </summary>
        public int Points
        {
            get
            {
                if (Fixture.HomeGoals != null && Fixture.AwayGoals != null)
                {
                    if (HomeGoals != null && AwayGoals != null)
                    {
                        if (Fixture.HomeGoals == HomeGoals && Fixture.AwayGoals == AwayGoals)
                        {
                            return 3;
                        } else if (Fixture.HomeGoals == Fixture.AwayGoals && HomeGoals == AwayGoals)
                        {
                            return 1;
                        } else if (
                            (Fixture.HomeGoals > Fixture.AwayGoals && HomeGoals > AwayGoals)
                            || (Fixture.AwayGoals < Fixture.HomeGoals && AwayGoals < HomeGoals)
                        )
                        {
                            return 1;
                        } else
                        {
                            return 0;
                        }
                    } else
                    {
                        return 0;
                    }
                } else
                {
                    return 0;
                }
            }
        }
    }
}