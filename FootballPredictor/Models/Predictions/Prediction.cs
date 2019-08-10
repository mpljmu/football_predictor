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

namespace FootballPredictor.Models.Predictions
{
    public abstract class Prediction : IPrediction
    {
        public int Id { get; }
        public IPlayer Player { get; protected set; }
        public IFixture Fixture { get; protected set; }
        public PredictionScore Score { get; protected set; }
        [Inject]
        public IDatabaseConnection DatabaseConnection { protected get; set; }
        [Inject]
        public ILogger Logger { protected get; set; }


        public Prediction(int id)
        {
            Id = id;
            NinjectWebCommon.Bootstrapper.Kernel.Inject(this);
        }
        public Prediction(int id, PredictionScore score)
        {
            Id = id;
            Score = score;
            NinjectWebCommon.Bootstrapper.Kernel.Inject(this);
        }
        public Prediction(IPlayer player, IFixture fixture, PredictionScore score)
        {
            Player = player;
            Fixture = fixture;
            Score = score;
        }


        public IPrediction GetFromDatabase()
        {
            IPrediction prediction = DatabaseConnection.Connection.Query<Prediction, Player, Fixture, Prediction>
            (
                @"SELECT
	                Prediction.Id,
	                HomeGoals,
	                AwayGoals,
	                SubmittedOn,
	                PlayerId,
	                FixtureId
                  FROM
		                Prediction
	                INNER JOIN Player
		                ON Prediction.PlayerId = Player.Id
	                INNER JOIN [User]
		                ON Player.UserId = [User].Id
                  WHERE
	                Prediction.Id = @Id",
                (thisPrediction, thisPlayer, thisFixture) =>
                {
                    thisPrediction.Player = thisPlayer;
                    thisPrediction.Fixture = thisFixture;
                    return thisPrediction;
                },
                new
                {
                    Id = Id
                },
                splitOn: "PlayerId,FixtureId"
            ).FirstOrDefault();
            return prediction;
        }
    }
}