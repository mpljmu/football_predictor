using FootballPredictor.Models.Competitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;

namespace FootballPredictor.Models.Predictions
{
    public class OpenPrediction : Prediction
    {
        public OpenPrediction(int id)
            : base(id)
        {

        }
        public OpenPrediction(IPlayer player, IFixture fixture, PredictionScore score)
            : base (player, fixture, score)
        {

        }


        public void Insert()
        {
            if (Fixture.OpenForPredictions)
            {
                try
                {
                    using (DatabaseConnection.Connection)
                    {
                        DatabaseConnection.Connection.Execute(
                            @"INSERT INTO Prediction(FixtureId, PlayerId, HomeGoals, AwayGoals, SubmittedOn)
                              VALUES
                                (@FixtureId, @PlayerId, @HomeGoals, @AwayGoals, @SubmittedOn)",
                            new
                            {
                                FixtureId = Fixture.Id,
                                PlayerId = Player.Id,
                                HomeGoals = Score.HomeGoals,
                                AwayGoals = Score.AwayGoals,
                                SubmittedOn = new Utilities.Utility().UKDateTime
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
        public void UpdateScore()
        {
            try
            {
                using (DatabaseConnection.Connection)
                {
                    DatabaseConnection.Connection.Execute(
                        @"UPDATE tblPrediction (homeGoals, awayGoals)
                          VALUES (@HomeGoals, @AwayGoals)
                          WHERE PredictionId = @PredictionId",
                        new
                        {
                            HomeGoals = Score.HomeGoals,
                            AwayGoals = Score.AwayGoals,
                            PredictionId = Id
                        }
                    );
                }
            }
            catch (Exception ex)
            {
                //TODO: Log: Logger.Insert(ex.Message, "Prediction", Id, Logger.Category.Error, DateTime.Now, player.User.Id);
            }
        }
    }
}