using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;

namespace FootballPredictor.Models.Competitions
{
    public class CompetitionSeason : ICompetitionSeason
    {
        public int Id { get; set; }
        public Competition Competition { get; private set; }
        public Season Season { get; private set; }

        public CompetitionSeason(int id)
        {
            Id = id;
        }

        public List<Fixture> GetFixtures()
        {
            var fixtures = new List<Fixture>();
            try
            {
                using (var connection = new DatabaseConnection().Connection)
                {
                    connection.Open();
                    fixtures = connection.Query<Fixture, FixtureScore, Club, Club, Fixture>(
                    (@"SELECT fixture.fixtureID Id,  CAST(CONVERT(VARCHAR(8), fixture.[fixtureDate], 112) + ' ' + CAST(fixture.[fixtureTime] AS VARCHAR(5)) AS DATE) [Date], fixture.homeGoals HomeGoals, fixture.awayGoals AwayGoals, homeClub.clubId Id, homeClub.clubName Name, awayClub.clubId Id, awayClub.clubName Name
                     FROM tblFixture fixture
                     INNER JOIN tblClub homeClub
                     ON fixture.homeClubId = homeClub.clubId
                     INNER JOIN tblClub awayClub
                     ON fixture.awayClubId = awayClub.clubId
                     ORDER BY fixtureDate DESC, fixtureTime"
                    ),
                    (fixture, fixtureScore, homeClub, awayClub) =>
                    {
                        // CHECK WHY THE SCORE OBJECT IS NULL - IS IT THE QUERY??
                        fixture = new Fixture(fixture.Id, homeClub, awayClub, fixture.Date, fixtureScore);
                        return fixture;
                    },
                    splitOn: "Id,HomeGoals,Id,Id").ToList();
                }
            }
            catch (Exception ex)
            {
                // TODO
            }
            return fixtures;
        }
        public IEnumerable<Player> GetPlayers()
        {

            var databaseConnection = new DatabaseConnection();
            using (databaseConnection.Connection)
            {
                try
                {
                    databaseConnection.Connection.Open();
                    var players = databaseConnection.Connection.Query<User>(
                        (@"SELECT PlayerId, PlayerUsername, PlayerForename, PlayerSurname
                          FROM vwPlayerCompetition
                          WHERE CompetitionId = @CompetitionId AND SeasonName = @SeasonName"
                        ),
                        new
                        {
                            CompetitionId = Competition.Id,
                            SeasonName = Season.Id
                        }
                    );
                }
                catch (Exception ex)
                {
                    // TODO
                }


            }
            return new List<Player>();
        }
        public void AddPlayer(int playerId)
        {
            // TO DO
        }
    }
}