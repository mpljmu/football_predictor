using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;

namespace FootballPredictor.Models.Competitions
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
        private Club HomeClub { get; set; }
        private Club AwayClub {get; set;}
        public DateTime Date { get; private set; }
        private FixtureScore Score { get; set; }

        public Fixture(int id, DateTime date)
        {
            Id = id;
        }

        public Fixture(int id, Club homeClub, Club awayClub, DateTime date, FixtureScore score)
        {
            Id = id;
            HomeClub = homeClub;
            AwayClub = awayClub;
            Date = date;
            Score = score;
        }
        
        public void UpdateFixture()
        {
            try
            {
                //string query = "SELECT * FROM tblFixture";
                //using (var connection = new DatabaseConnection(query).Connection)
                //{
                //    connection.Open();

                //}
            } catch
            {

            }
        }
        public int CalculatePredictionPoints(int homeGoals, int awayGoals)
        {
            if (homeGoals == Score.HomeGoals && awayGoals == Score.AwayGoals)
            {
                return (int)Points.CorrectScore;
            }
            else if (homeGoals == awayGoals && Score.HomeGoals == Score.AwayGoals)
            {
                return (int)Points.CorrectOutcome;
            }
            else if (
                (homeGoals > awayGoals && Score.HomeGoals > Score.AwayGoals)
                || (awayGoals < homeGoals && Score.AwayGoals < Score.HomeGoals)
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
        public byte HomeGoals { get; private set; }
        public byte AwayGoals { get; private set; }
        public string Score
        {
            get
            {
                return string.Format("{0}-{1}", HomeGoals, AwayGoals);
            }
        }

        public FixtureScore(byte homeGoals, byte awayGoals)
        {
            HomeGoals = homeGoals;
            AwayGoals = awayGoals;
        }

    }

}