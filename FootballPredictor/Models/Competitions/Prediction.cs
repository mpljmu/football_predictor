using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using FootballPredictor.Models.Connections;
using FootballPredictor.Models.People;
using Dapper;
using System.Transactions;

namespace FootballPredictor.Models.Competitions
{
    public class Prediction
    {
        public int Id { get; }
        public User User { get; private set; }
        public Fixture Fixture { get; private set; }
        public int HomeGoals { get; private set; }
        public int AwayGoals { get; private set; }
        public int Points
        {
            get
            {
                return Fixture.CalculatePredictionPoints(HomeGoals, AwayGoals);
            }
        }
        public IDatabaseConnection DatabaseConnection { get; set; }


        public Prediction(int id, IDatabaseConnection databaseConnection)
        {
            Id = id;
            DatabaseConnection = databaseConnection;
        }
        public Prediction(int id, User user, Fixture fixture, int homeGoals, int awayGoals)
        {
            Id = id;
            User = user;
            Fixture = fixture;
            HomeGoals = homeGoals;
            AwayGoals = awayGoals;
        }


        public void PopulateObject()
        {
            DatabaseConnection.Command.CommandText = 
                @"SELECT *
                  FROM [Use a view for the prediction]"
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