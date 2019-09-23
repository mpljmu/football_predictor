using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using FootballPredictor.Models.Connections;
using FootballPredictor.Models.People;
using FootballPredictor.Models.Loggers;
using Dapper;
using System.Transactions;
using Ninject;
using FootballPredictor.App_Start;
using FootballPredictor.Models.Competitions;
using FootballPredictor.Models.Fixtures;

namespace FootballPredictor.Models.Predictions
{
    public class Prediction : IPrediction
    {
        public int Id { get; set; }
        public IPlayer Player { get; set; }
        public IFixture Fixture { get; set; }
        public IPredictionScore Score { get; set; }


        public Prediction(int id)
        {
            Id = id;
        }
        public Prediction(int id, IPredictionScore score)
        {
            Id = id;
            Score = score;
        }
        public Prediction(int id, IPlayer player, IFixture fixture, IPredictionScore score)
        {
            Id = id;
            Player = player;
            Fixture = fixture;
            Score = score;
        }


    }
}