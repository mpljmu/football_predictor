using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace football_predictor.Models
{
    public class Prediction
    {
        public int Id { get; }
        private User _user;
        private Fixture _fixture;
        private int _homeGoals;
        private int _awayGoals;
        public int Points
        {
            get
            {
                return _fixture.CalculatePredictionPoints(_homeGoals, _awayGoals);
            }
        }

        public Prediction(int id, User user, Fixture fixture, int homeGoals, int awayGoals)
        {
            Id = id;
            _user = user;
            _fixture = fixture;
            _homeGoals = homeGoals;
            _awayGoals = awayGoals;
        }
        public void InsertPrediction()
        {
            
            // insert prediction in to the database
        }

        public void UpdatePrediction()
        {
            // update the goals for a prediction - use the internal id
        }

    }
}