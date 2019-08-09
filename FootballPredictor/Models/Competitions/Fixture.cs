using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using FootballPredictor.Models.Clubs;

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
        public IClub HomeClub { get; private set; }
        public IClub AwayClub {get; set;}
        public DateTime Date { get; private set; }
        private FixtureScore Score { get; set; }


        public Fixture(int id)
        {
            Id = id;
        }
        public Fixture(int id, IClub homeClub, IClub awayClub, DateTime date, FixtureScore score)
        {
            Id = id;
            HomeClub = homeClub;
            AwayClub = awayClub;
            Date = date;
            Score = score;
        }
        

        public void UpdateScore()
        {
            try
            {
                
            } catch
            {

            }
        }
        public void UpdateDate()
        {

        }
        public int CalculatePredictionPoints(PredictionScore predictionScore)
        {
            if (Score.HomeGoals == predictionScore.HomeGoals && Score.AwayGoals == predictionScore.AwayGoals)
            {
                return (int)Points.CorrectScore;
            }
            else if (Score.HomeGoals == Score.AwayGoals && predictionScore.HomeGoals == predictionScore.AwayGoals)
            {
                // Correctly guessed a draw
                return (int)Points.CorrectOutcome;
            }
            else if (
                (Score.HomeGoals > Score.AwayGoals && predictionScore.HomeGoals > predictionScore.AwayGoals)
                || (Score.AwayGoals < Score.HomeGoals && predictionScore.AwayGoals < predictionScore.HomeGoals)
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