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
    public class PlayerRepository : IPlayerRepository
    {
        private IDatabaseConnection DatabaseConnection { get; set; }


        public PlayerRepository(IDatabaseConnection databaseConnection)
        {
            DatabaseConnection = databaseConnection;
        }


        public IPlayer Get(int id)
        {
            try
            {
                using (var connection = DatabaseConnection.NewConnection())
                {
                    var player = connection.Query<Player, User, Player>(
                        @"SELECT
	                        PlayerId Id,
	                        [User].Id Id,
                            [User].Username,
                            [User].Forename,
                            [User].Surname
                          FROM
		                        vwPlayers
	                        INNER JOIN Player
		                        ON vwPlayers.PlayerId = Player.Id
	                        INNER JOIN [User]
		                        ON vwPlayers.UserId = [User].Id
                          WHERE
                            vwPlayers.PLayerId = @PlayerId",
                        (thisPlayer, thisUser) =>
                        {
                            thisPlayer.User = thisUser;
                            return thisPlayer;
                        },
                        new
                        {
                            PlayerId = id,
                        }
                    ).FirstOrDefault();
                    player.CompetitionSeason = new Models.Competitions.CompetitionSeason(id);
                    return player;
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
        public IEnumerable<IPlayer> GetAllByCompetitionSeason(int competitionSeasonId)
        {
            try
            {
                using (var connection = DatabaseConnection.NewConnection())
                {
                    var players = connection.Query<Player, User, Player>(
                        @"SELECT
	                        PlayerId Id,
	                        [User].Id Id,
                            [User].Username,
                            [User].Forename,
                            [User].Surname
                          FROM
		                        vwPlayers
	                        INNER JOIN Player
		                        ON vwPlayers.PlayerId = Player.Id
	                        INNER JOIN [User]
		                        ON vwPlayers.UserId = [User].Id
                          WHERE
                            vwPlayers.CompetitionSeasonId = @CompetitionSeasonId",
                        (thisPlayer, thisUser) =>
                        {
                            thisPlayer.User = thisUser;
                            return thisPlayer;
                        },
                        new
                        {
                            CompetitionSeasonId = competitionSeasonId,
                        }
                    ).ToList();
                    return players;
                }
            }
            catch (Exception ex)
            {
                // TODO: Log
                throw ex;
            }
        }
    }
}