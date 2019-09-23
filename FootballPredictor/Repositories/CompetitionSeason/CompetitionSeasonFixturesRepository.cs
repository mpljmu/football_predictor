using Dapper;
using FootballPredictor.Models.Clubs;
using FootballPredictor.Models.Connections;
using FootballPredictor.Models.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPredictor.Repositories.CompetitionSeason
{
    public class CompetitionSeasonFixturesRepository : ICompetitionSeasonFixturesRepository
    {
        private IDatabaseConnection DatabaseConnection { get; set; }


        public CompetitionSeasonFixturesRepository(IDatabaseConnection databaseConnection)
        {
            DatabaseConnection = databaseConnection;
        }


        public IEnumerable<IFixture> Get(int competitionSeasonId)
        {
            try
            {
                using (var connection = DatabaseConnection.NewConnection())
                {
                    var fixtures = connection.Query<Fixture, FixtureScore, Club, Club, Fixture>(
                        @"SELECT
	                        FixtureId Id,
	                        FixtureDate [Date],
	                        FixtureCompleted Completed,
	                        HomeGoals,
	                        AwayGoals,
	                        HomeClubId Id,
                            HomeClubName Name,
	                        AwayClubId Id,
                            AwayClubName Name
                          FROM
		                    vwFixtureClubs
                          WHERE
                            CompetitionSeasonId = @CompetitionSeasonId",
                        (fixture, fixtureScore, homeClub, awayClub) =>
                        {
                            fixture = new Fixture(fixture.Id, fixture.Completed, homeClub, awayClub, fixture.Date, fixtureScore);
                            return fixture;
                        },
                        new
                        {
                            CompetitionSeasonId = competitionSeasonId
                        },
                        splitOn: "Id,HomeGoals,Id,Id"
                    ).ToList();
                    return fixtures;
                }
            }
            catch (Exception ex)
            {
                // TODO
                throw ex;
            }
        }
    }
}