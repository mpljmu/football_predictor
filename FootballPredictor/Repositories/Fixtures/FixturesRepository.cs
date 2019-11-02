using FootballPredictor.Models.Connections;
using FootballPredictor.Models.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using FootballPredictor.Models.Clubs;
using System.Transactions;

namespace FootballPredictor.Repositories.Fixtures
{
    public class FixturesRepository : IFixturesRepository
    {
        private IDatabaseConnection DatabaseConnection { get; set; }

        public FixturesRepository(IDatabaseConnection databaseConnection)
        {
            DatabaseConnection = databaseConnection;
        }


        public IFixture Get(int id)
        {
            try
            {
                using (var connection = DatabaseConnection.NewConnection())
                {
                    var fixture = connection.Query<Fixture, FixtureScore, Club, Club, Fixture>(
                        @"SELECT
                            FixtureId Id,
                            FixtureDate[Date],
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
                            FixtureId = @FixtureId",
                        (thisFixture, fixtureScore, homeClub, awayClub) =>
                        {
                            thisFixture = new Fixture(thisFixture.Id, thisFixture.Completed, homeClub, awayClub, thisFixture.Date, fixtureScore);
                            return thisFixture;
                        },
                        new
                        {
                            FixtureId = id
                        },
                        splitOn: "Id,HomeGoals,Id,Id"
                    ).FirstOrDefault();
                    return fixture;
                }
            }
            catch (Exception ex)
            {
                // TODO: Log
                throw ex;
            }
        }
        public void Create(IFixture fixture)
        {

        }
        public void Insert(IFixture fixture)
        {

        }
        public void UpdateScore(int id, IFixtureScore score)
        {
            try
            {
                using (var transactionScope = new TransactionScope())
                {
                    using (var connection = DatabaseConnection.NewConnection())
                    {
                        // Check for existing score record
                        var currentScore = GetScore(id);

                        if (currentScore != null)
                        {
                            // Update the existing record
                            connection.Execute(
                                @"UPDATE FixturScore
                                  SET
                                    HomeGoals = @HomeGoals,
                                    AwayGoals = @AwayGoals
                                  WHERE
                                    FixtureId = @FixtureId",
                                new
                                {
                                    FixtureId = id
                                }
                            );
                        }
                        else
                        {
                            // One doesn't exist, insert the score record
                            connection.Execute(
                                @"UPDATE Fixture
                              SET
                                HomeGoals = @HomeGoals,
                                AwayGoals = @AwayGoals
                              WHERE
                                FixtureId = @FixtureId",
                                new
                                {
                                    HomeGoals = score.HomeGoals,
                                    AwayGoals = score.AwayGoals,
                                    FixtureId = id
                                }
                            );
                        }
                        transactionScope.Complete();
                    }
                }

            }
            catch (Exception ex)
            {
                // TODO: Log
                throw ex;
            }
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
        public void Complete(int id)
        {
            try
            {
                using (var transactionScope = new TransactionScope())
                {
                    using (var connection = DatabaseConnection.NewConnection())
                    {
                        // First check there is a score before setting to completed
                        var fixtureScore = connection.Query<FixtureScore>(
                            @"SELECT
                                HomeGoals,
                                AwayGoals
                              FROM
                                Fixture
                              WHERE
                                FixtureId = @FixtureId",
                            new
                            {
                                FixtureId = id
                            }
                        ).FirstOrDefault();

                        if (fixtureScore == null)
                        {
                            throw new Exception("The fixture must have a score before it can be completed");
                        }

                        // There is a score entry, so the fixture can be completed. First check if it has already been completed
                        var completed = IsCompleted(id);

                        if (completed)
                        {
                            throw new Exception("Fixture is already completed");
                        }

                        // Has a score and isn't already completed, complete the fixture
                        connection.Execute(
                            @"UPDATE FixtureScore
                              SET Completed = 1
                              WHERE FixtureId = @FixtureId",
                            new
                            {
                                FixtureId = id
                            }
                        );
                        transactionScope.Complete();
                    }
                }

            }
            catch (Exception ex)
            {
                // TODO: Log
                throw ex;
            }
        }
        public void Uncomplete(int id)
        {
            try
            {
                using (var transactionScope = new TransactionScope())
                {
                    using (var connection = DatabaseConnection.NewConnection())
                    {
                        // Check if the fixture is completed, if not then do not update to uncompleted
                        var completed = IsCompleted(id);

                        if (completed)
                        {
                            connection.Execute(
                               @"UPDATE Fixture
                                    SET
                                    Completed = 0
                                  WHERE
                                    FixtureId = @FixtureId",
                               new
                               {
                                   FixtureId = id
                               }
                            );
                        } else
                        {
                            throw new Exception("Fixture is not completed");
                        }
                        transactionScope.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                // TODO: Log
                throw ex;
            }
        }
        public bool IsCompleted(int id)
        {
            try
            {
                using (var connection = DatabaseConnection.NewConnection())
                {
                    var completed = connection.Query<bool>(
                        @"SELECT
                            Completed
                          FROM
                            FixtureScore
                          WHERE
                            FixtureId = @FixtureId",
                        new
                        {
                            FixtureId = id
                        }
                    ).FirstOrDefault();
                    return completed;
                }
            }
            catch (Exception ex)
            {
                // TODO: Log
                throw ex;
            }
        }
        public IFixtureScore GetScore(int id)
        {
            try
            {
                using (var connection = DatabaseConnection.NewConnection())
                {
                    var score = connection.QueryFirst<FixtureScore>(
                        @"SELECT
                            HomeGoals,
                            AwayGoals
                          WHERE
                            FixtureId = @FixtureId",
                        new
                        {
                            FixtureId = id
                        }
                    );
                    return score;
                }
            }
            catch (Exception ex)
            {
                // TODO: Log
                throw ex;
            }
        }
        public void SetDate(int id, DateTime date)
        {
            try
            {
                using (var connection = DatabaseConnection.NewConnection())
                {
                    connection.Execute(
                        @"UPDATE Fixture
                          SET
                            Date = @Date",
                        new
                        {
                            Date = date
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
        public IEnumerable<IFixture> GetByCompetitionSeason(int competitionSeasonId)
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
        public IEnumerable<IFixture> GetCompletedByCompetitionSeason(int competitionSeasonId)
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
                            CompetitionSeasonId = @CompetitionSeasonId
                            AND FixtureCompleted = 1",
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

        /// <summary>
        /// Also includes the live fixtures, are these are incomplete
        /// </summary>
        /// <param name="competitionSeasonId"></param>
        /// <returns></returns>
        public IEnumerable<IFixture> GetIncompleteByCompetitionSeason(int competitionSeasonId)
        {
            try
            {
                using (var connection = DatabaseConnection.NewConnection())
                {
                    var fixtures = connection.Query<Fixture, FixtureScore, Club, Club, Fixture>(
                        @"SELECT
	                        FixtureId Id,
	                        FixtureCompleted Completed,
	                        FixtureDate [Date],
	                        HomeGoals,
	                        AwayGoals,
	                        HomeClubId Id,
                            HomeClubName Name,
	                        AwayClubId Id,
                            AwayClubName Name
                          FROM
		                    vwFixtureClubs
                         WHERE
                            CompetitionSeasonId = @CompetitionSeasonId
                            AND FixtureCompleted = 0",
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
        public IEnumerable<IFixture> GetLiveByCompetitionSeason(int competitionSeasonId)
        {
            try
            {
                var incompleteFixtures = GetIncompleteByCompetitionSeason(competitionSeasonId);
                var liveFixtures = new List<IFixture>();
                foreach (var incompleteFixture in incompleteFixtures)
                {
                    if (!incompleteFixture.OpenForPredictions)
                    {
                        liveFixtures.Add(incompleteFixture);
                    }
                }
                return liveFixtures;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}