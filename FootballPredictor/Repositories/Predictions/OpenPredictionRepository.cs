using FootballPredictor.Models.Competitions;
using FootballPredictor.Models.Connections;
using FootballPredictor.Models.Predictions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using FootballPredictor.Models.Utilities;
using FootballPredictor.Models.Fixtures;
using FootballPredictor.Models.People;

namespace FootballPredictor.Repositories.Predictions
{
    public class OpenPredictionRepository : IOpenPredictionRepository
    {
        private IDatabaseConnection DatabaseConnection { get; set; }


        public OpenPredictionRepository(IDatabaseConnection databaseConnection)
        {
            DatabaseConnection = databaseConnection;
        }


        public void UpdateScore(int id, IPredictionScore score)
        {
            try
            {
                using (var connection = DatabaseConnection.NewConnection())
                {
                    connection.Execute(
                        @"UPDATE Prediction(homeGoals, awayGoals)
                          VALUES (@HomeGoals, @AwayGoals)
                          WHERE PredictionId = @PredictionId",
                        new
                        {
                            HomeGoals = score.HomeGoals,
                            AwayGoals = score.AwayGoals,
                            PredictionId = id
                        }
                    );
                }
            }
            catch (Exception ex)
            {
                //TODO: Log: Logger.Insert(ex.Message, "Prediction", Id, Logger.Category.Error, DateTime.Now, player.User.Id);
                throw ex;
            }
        }
        public void Insert(IOpenPrediction prediction)
        {
            if (prediction.Fixture.OpenForPredictions)
            {
                try
                {
                    using (var connection = DatabaseConnection.NewConnection())
                    {
                        connection.Execute(
                            @"INSERT INTO Prediction(FixtureId, PlayerId, HomeGoals, AwayGoals, SubmittedOn)
                              VALUES
                                (@FixtureId, @PlayerId, @HomeGoals, @AwayGoals, @SubmittedOn)",
                            new
                            {
                                FixtureId = prediction.Fixture.Id,
                                PlayerId = prediction.Player.Id,
                                HomeGoals = prediction.Score.HomeGoals,
                                AwayGoals = prediction.Score.AwayGoals,
                                SubmittedOn = new Utility().UKDateTime
                            }
                        );
                    }
                }
                catch (Exception ex)
                {
                    // TODO: Log
                    throw ex;
                }
            }
        }
        public void Delete(int id)
        {
            try
            {
                using (var connection = DatabaseConnection.NewConnection())
                {
                    connection.Execute(
                        @"DELETE Prediction
                          WHERE Id = @Id",
                        new
                        {
                            Id = id
                        }
                    );
                }
            }
            catch (Exception ex)
            {
                // TODO: Log
                throw ex;
            }
        }
        public IOpenPrediction Get(int id)
        {
            try
            {
                using (var connection = DatabaseConnection.NewConnection())
                {
                    IOpenPrediction prediction = connection.Query<OpenPrediction, Player, Fixture, OpenPrediction>(
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
                            Id = id
                        },
                        splitOn: "PlayerId,FixtureId"
                    ).FirstOrDefault();
                    return prediction;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}