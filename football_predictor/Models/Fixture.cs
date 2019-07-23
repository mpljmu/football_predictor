using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;

namespace FootballPredictor.Models
{
    public class Fixture
    {
        private enum Points
        {
            CorrectScore = 3,
            CorrectOutcome = 1,
            Incorrect = 0
        }

        public int Id { get; private set; }
        public Club HomeClub { get; set; }
        private Club _awayClub;
        private DateTime _date;
        private FixtureScore _score;

        public Fixture(int id)
        {
            Id = id;
        }

        public Fixture(int id, Club homeClub, Club awayClub, DateTime date, FixtureScore score)
        {
            Id = id;
            HomeClub = homeClub;
            _awayClub = awayClub;
            _date = date;
            _score = score;
        }
        
        public void UpdateFixture()
        {
            try
            {
                string query = "SELECT * FROM tblFixture";
                using (var connection = new DatabaseConnection(query).Connection)
                {
                    connection.Open();

                }
            } catch
            {

            }
        }
        public int CalculatePredictionPoints(int homeGoals, int awayGoals)
        {
            if (homeGoals == _score.HomeGoals && awayGoals == _score.AwayGoals)
            {
                return (int)Points.CorrectScore;
            }
            else if (homeGoals == awayGoals && _score.HomeGoals == _score.AwayGoals)
            {
                return (int)Points.CorrectOutcome;
            }
            else if (
                (homeGoals > awayGoals && _score.HomeGoals > _score.AwayGoals)
                || (awayGoals < homeGoals && _score.AwayGoals < _score.HomeGoals)
            )
            {
                return (int)Points.CorrectOutcome;
            }
            else
            {
                return (int)Points.Incorrect;
            }
        }

    }

    public class FixtureScore
    {
        public int HomeGoals { get;  }
        public int AwayGoals { get;  }
        public string Score
        {
            get
            {
                return string.Format("{0}-{1}", HomeGoals, AwayGoals);
            }
        }

        public FixtureScore(int homeGoals, int awayGoals)
        {
            HomeGoals = homeGoals;
            AwayGoals = awayGoals;
        }
    }

}