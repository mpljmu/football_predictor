using Dapper;
using FootballPredictor.Models.Clubs;
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
                            if (prediction.Fixture.OpenForPredictions)
                            {
                                prediction = new OpenPrediction(prediction.Id, player, fixture, predictionScore);
                            } else
                            {
                                prediction = new ClosedPrediction(prediction.Id, player, fixture, predictionScore);
                            }
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
        public IEnumerable<IPrediction> GetByCompetitionSeasonPlayer(int competitionSeasonId, int playerId)
        {
            try
            {
                using (var connection = DatabaseConnection.NewConnection())
                {
                    var predictions = connection.Query<Prediction>(
                        @"SELECT
                            vwPlayerPredictions.PredictionId Id,
	                        vwPlayerPredictions.PlayerId Id,
	                        vwPlayerPredictions.UserId Id,
	                        vwFixtureClubs.FixtureId Id,
	                        ISNULL(vwFixtureClubs.FixtureCompleted, 0) Completed,
                            vwFixtureClubs.FixtureDate [Date],
	                        vwFixtureClubs.HomeClubId Id,
	                        vwFixtureClubs.HomeClubName [Name],
	                        vwFixtureClubs.AwayClubId Id,
	                        vwFixtureClubs.AwayClubName [Name],
	                        vwFixtureClubs.HomeGoals,
	                        vwFixtureClubs.AwayGoals,
	                        vwPlayerPredictions.PredictionHomeGoals HomeGoals,
	                        vwPlayerPredictions.PredictionAwayGoals AwayGoals
                          FROM
		                        vwPlayerPredictions
	                        INNER JOIN vwFixtureClubs
		                        ON vwPlayerPredictions.FixtureId = vwFixtureClubs.FixtureId		
                          WHERE
	                        vwPlayerPredictions.CompetitionSeasonId = @CompetitionSeasonId
	                        AND vwPlayerPredictions.PlayerId = @PlayerId",
                        new[] { typeof(Prediction), typeof(Player), typeof(User), typeof(Fixture), typeof(Club), typeof(Club), typeof(FixtureScore), typeof(PredictionScore) },
                        (objects) =>
                        {
                            var prediction = (Prediction)objects[0];
                            var player = (Player)objects[1];
                            var user = (User)objects[2];
                            var fixture = (Fixture)objects[3];
                            var homeClub = (Club)objects[4];
                            var awayClub = (Club)objects[5];
                            var fixtureScore = (FixtureScore)objects[6];
                            var predictionScore = (PredictionScore)objects[7];

                            player = new Player(
                                player.Id,
                                user
                            );
                            fixture = new Fixture(
                                fixture.Id,
                                fixture.Completed,
                                homeClub,
                                awayClub,
                                fixture.Date,
                                fixtureScore
                            );

                            if (fixture.OpenForPredictions)
                            {
                                prediction = new OpenPrediction(
                                    prediction.Id,
                                    player,
                                    fixture,
                                    predictionScore
                                );
                            }
                            else
                            {
                                prediction = new ClosedPrediction(
                                    prediction.Id,
                                    player,
                                    fixture,
                                    predictionScore
                                );
                            }

                            return prediction;
                        },
                        new
                        {
                            CompetitionSeasonId = competitionSeasonId,
                            PlayerId = playerId
                        },
                        splitOn: "Id,Id,Id,Id,Id,Id,HomeGoals,HomeGoals"
                    ).ToList();
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