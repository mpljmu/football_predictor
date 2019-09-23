using FootballPredictor.Models.Competitions;
using FootballPredictor.Models.Connections;
using FootballPredictor.Models.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using FootballPredictor.Models.Predictions;
using FootballPredictor.Models.Fixtures;

namespace FootballPredictor.Repositories.CompetitionSeason
{
    public class CompetitionSeasonPlayersRepository : ICompetitionSeasonPlayersRepository
    {
        private IDatabaseConnection DatabaseConnection { get; set; }


        public CompetitionSeasonPlayersRepository(IDatabaseConnection databaseConnection)
        {
            DatabaseConnection = databaseConnection;
        }


        public IEnumerable<IPlayer> Get(int id)
        {
            try
            {
                using (var connection = DatabaseConnection.NewConnection())
                {
                    var players = connection.Query<Player, User, Player>(
                        @"SELECT
	                        PlayerId Id,
	                        [User].Id UserId
                          FROM
		                        vwPlayers
	                        INNER JOIN Player
		                        ON vwPlayers.PlayerId = Player.Id
	                        INNER JOIN [User]
		                        ON vwPlayers.UserId = [User].Id
                          WHERE
                            CompetitionSeasonId = @CompetitionSeasonId",
                        (thisPlayer, thisUser) => {
                            thisPlayer.User = thisUser;
                            return thisPlayer;
                        },
                        new
                        {
                            CompetitionSeasonId = id,
                        }
                    ).ToList();
                    // For each player, add the competition season property
                    foreach (var player in players)
                    {
                        player.CompetitionSeason = new Models.Competitions.CompetitionSeason(id);

                    }
                    return players;
                }
            }
            catch (Exception ex)
            {
                // TODO: Log
                throw ex;
            }
        }
        public void Add(int id, IPlayer player)
        {
            // SQL to add the player to the competition;
        }
        public void Delete(int id, int playerId)
        {
            // SQL to remove the player from the competition season
        }
        public IEnumerable<IPrediction> GetPredictions(int id, int playerId)
        {
            try
            {
                using (var connection = DatabaseConnection.NewConnection())
                {

                    // USE THE TABLE VIEW WHICH HAS BEEN CREATED TO DEVELOP THIS METHOD - VIEW MIGHT NEED AMENDING
                    var predictions = connection.Query<Prediction, Player, Fixture, PredictionScore, Prediction>(
                        @"SELECT
                            PredictionId Id,
                            PlayerId Id,
                            FixtureId Id,
                            PredictionHomeGoals HomeGoals,
                            PredictionAwayGoals AwayGoals
                          FROM
                            vwPlayerPredictions
                          WHERE
                            CompetitionSeasonId = @CompetitionSeasonId
                            AND PlayerId = @PlayerId",
                        (thisPrediction, thisPlayer, thisFIxture, thisPredictionScore) =>
                        {
                            thisPrediction.Player = thisPlayer;
                            thisPrediction.Fixture = thisFIxture;
                            thisPrediction.Score = thisPredictionScore;
                            return thisPrediction;
                        },
                        new
                        {
                            CompetitionSeasonId = id,
                            PlayerId = playerId
                        }
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