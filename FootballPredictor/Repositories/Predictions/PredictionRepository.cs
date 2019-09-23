using Dapper;
using FootballPredictor.Models.Connections;
using FootballPredictor.Models.Fixtures;
using FootballPredictor.Models.People;
using FootballPredictor.Models.Predictions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPredictor.Repositories.Predictions
{
    public class PredictionRepository : IPredictionRepository
    {
        private IDatabaseConnection DatabaseConnection { get; set; }

        public PredictionRepository(IDatabaseConnection databaseConnection)
        {
            DatabaseConnection = databaseConnection;
        }

        public IEnumerable<IPrediction> GetByCompetitionSeasonFixture(int competitionSeasonId, int fixtureId)
        {
            try
            {
                using (var connection = DatabaseConnection.NewConnection())
                {
                    var predictions = connection.Query<Prediction, Player, User, Fixture, PredictionScore, Prediction>(
                        @"SELECT   
                            PredictionId Id,
                            PlayerId Id,
							[User].Id Id,
							[User].Username,
							[User].Forename,
							[User].Surname,
                            Fixture.Id Id,
							Fixture.[Date] [Date],
							FixtureSCore.Completed,
                            PredictionHomeGoals HomeGoals,
                            PredictionAwayGoals AwayGoals
                          FROM
								vwPlayerPredictions
							INNER JOIN [User]
								ON vwPlayerPredictions.UserId = [User].Id
							INNER JOIN Fixture
								ON vwPlayerPredictions.FixtureId = Fixture.Id
							LEFT OUTER JOIN FixtureScore
								ON Fixture.Id = FixtureScore.FixtureId
                          WHERE
                            CompetitionSeasonId = @CompetitionSeasonId
                            AND Fixture.Id = @FixtureId",
                        (prediction, player, user, fixture, predictionScore) =>
                        {
                            player = new Player(player.Id, user);
                            prediction = new Prediction(prediction.Id, player, fixture, predictionScore);
                            return prediction;
                        },
                        new
                        {
                            CompetitionSeasonId = competitionSeasonId,
                            FixtureId = fixtureId
                        },
                        splitOn:"Id,Id,Id,Id,HomeGoals"
                    );
                    return predictions;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}