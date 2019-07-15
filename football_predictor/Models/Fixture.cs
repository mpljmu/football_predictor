using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;

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

        public int Id { get; }
        private Club _homeClub;
        private Club _awayClub;
        private DateTime _date;
        private int? _homeGoals;
        private int? _awayGoals;
        public string Score {
            get
            {
                if (_homeGoals != null && _awayGoals != null)
                {
                    return string.Format("{0}-{1}", _homeGoals, _awayGoals);
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public Fixture(int fixtureID)
        {
            Id = fixtureID;
        }

        public Fixture(int id, Club homeClub, Club awayClub, DateTime date, int? homeGoals, int? awayGoals)
        {
            Id = id;
            _homeClub = homeClub;
            _awayClub = awayClub;
            _date = date;
            _homeGoals = homeGoals;
            _awayGoals = awayGoals;
        }
        
        public void UpdateFixture()
        {
            SqlConnection sqlConnection = Connection.DatabaseConnection;
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM tblFixture", sqlConnection);

            using(sqlConnection)
            {
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                DataTable result = new DataTable();
                if (sqlDataReader.HasRows)
                {
                    result.Load(sqlDataReader);
                };


            }
                

            //SqlCommand sqlCommand = new SqlCommand("UPDATE tblFixture")
            // Connect to the database and set the value
        }

        public static IEnumerable<Fixture> GetAllFixtures(string season, string competition)
        {

            SqlConnection sqlConnection = Connection.DatabaseConnection;
            using (sqlConnection) {
                sqlConnection.Open();
                return sqlConnection.Query<Fixture>
                ("SELECT fixtureID FROM tblFixture").ToList();
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
            if (homeGoals == _homeGoals && awayGoals == _awayGoals)
            {
                return (int)Points.CorrectScore;
            }
            else if (homeGoals == awayGoals && _homeGoals == _awayGoals)
            {
                return (int)Points.CorrectOutcome;
            }
            else if (
                (homeGoals > awayGoals && _homeGoals > _awayGoals)
                || (awayGoals < homeGoals && _awayGoals < _homeGoals)
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
}