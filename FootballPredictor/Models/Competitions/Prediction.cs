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

namespace FootballPredictor.Models.Competitions
{
    public class Prediction : IPrediction
    {
        public int Id { get; }
        public IPlayer User { get; private set; }
        public IFixture Fixture { get; private set; }
        public PredictionScore Score { get; private set; }
        public int Points
        {
            get
            {
                return Fixture.CalculatePredictionPoints(HomeGoals, AwayGoals);
            }
        }
        [Inject]
        public IDatabaseConnection DatabaseConnection { private get; set; }
        [Inject]
        public ILogger Logger { private get; set; }


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


        public IPrediction GetFromDatabase<T>()
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
                (thisPrediction, thisUser, thisFixture) =>
                {
                    thisPrediction.User = thisUser;
                    thisPrediction.Fixture = thisFixture;
                    return thisPrediction;
                },
                new
                {
                    Id = Id
                },
                splitOn:""
            ).FirstOrDefault();
            return prediction;
        }

        public void Insert()
        {
            
        }
        public void UpdateScore(int homeGoals, int awayGoals, Player player)
        {
            try
            {
                using (DatabaseConnection.Connection)
                {
                    DatabaseConnection.Command.CommandText =
                        @"UPDATE tblPrediction (homeGoals, awayGoals)
                          VALUES (@HomeGoals, @AwayGoals)
                          WHERE PredictionId = @PredictionId";
                    DatabaseConnection.Connection.Execute(
                        DatabaseConnection.Command.CommandText,
                        new
                        {
                            HomeGoals = homeGoals,
                            AwayGoals = awayGoals,
                            PredictionId = Id
                        }
                    );
                }
                Logger.Insert(String.Format("Update prediction score. New score {0}-{1}", HomeGoals, AwayGoals), "Prediction", Id, Logger.Category.Success, DateTime.Now, player.User.Id);
            }
            catch (Exception ex)
            {
                Logger.Insert(ex.Message, "Prediction", Id, Logger.Category.Error, DateTime.Now, player.User.Id);
            }
        }

    }
}