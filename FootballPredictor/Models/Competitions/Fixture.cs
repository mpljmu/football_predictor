using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using FootballPredictor.Models.Clubs;
using Ninject;
using FootballPredictor.Models.Connections;
using FootballPredictor.App_Start;
using FootballPredictor.Models.Predictions;

namespace FootballPredictor.Models.Competitions
{
    public class Fixture : IFixture
    {
        public int Id { get; protected set; }
        public IClub HomeClub { get; protected set; }
        public IClub AwayClub { get; set; }
        public DateTime Date { get; protected set; }
        public FixtureScore Score { get; protected set; }
        /// <summary>
        /// Ended and has a full-time result when it is then marked as completed. Allows for in-play score updates
        /// </summary>
        public bool Completed { get; protected set; }
        /// <summary>
        /// Open for predictions if it has not started yet
        /// </summary>
        public bool OpenForPredictions
        {
            get
            {
                if (Date > new Utilities.Utility().UKDateTime)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        [Inject]
        public IDatabaseConnection DatabaseConnection { private get; set; }


        public Fixture(int id)
        {
            Id = id;
        }
        public Fixture(int id, IClub homeClub, IClub awayClub, DateTime date, FixtureScore score)
        {
            Id = id;
            HomeClub = homeClub;
            AwayClub = awayClub;
            Date = date;
            Score = score;
        }
        public Fixture(int id, bool completed)
        {
            Id = id;
            Completed = completed;
            NinjectWebCommon.Bootstrapper.Kernel.Inject(this);
        }


        public IFixture GetFromDatabase()
        {
            try
            {
                using (DatabaseConnection.Connection)
                {
                    IFixture fixture = DatabaseConnection.Connection.Query<Fixture, Club, Club, FixtureScore, Fixture>(
                        @"SELECT
	                        FixtureId,
	                        FixtureDate,
	                        HomeClubId,
	                        HomeClubName,
	                        AwayClubId,
	                        AwayClubName
                          FROM
	                        vwFixtureClubs
                          WHERE
                            FixtureId = Id",
                        (thisFixture, thisHomeClub, thisAwayClub, thisScore) =>
                        {
                            thisFixture.HomeClub = thisHomeClub;
                            thisFixture.AwayClub = thisAwayClub;
                            thisFixture.Score = thisScore;
                            return thisFixture;
                        },
                        new
                        {
                            Id = Id
                        }
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
        public void UpdateScore()
        {
            try
            {
                using (DatabaseConnection.Connection)
                {
                    DatabaseConnection.Connection.Execute(
                        @"UPDATE Fixture
                          SET
                            HomeGoals = @homeGoals
                            AwayGoals = @awayGoals
                          WHERE
                            Id = @Id",
                        new
                        {
                            HomeGoals = Score.HomeGoals,
                            AwayGoas = Score.AwayGoals,
                            Id = Id
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
        public void UpdateDate()
        {
            try
            {
                using (DatabaseConnection.Connection)
                {
                    DatabaseConnection.Connection.Execute(
                        @"UPDATE Fixture
                          SET
                            Date = @Date
                          WHERE
                            Id = Id",
                        new
                        {
                            Date = Date,
                            Id = Id
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
        public void SetCompletedStatus()
        {
            try
            {
                using (DatabaseConnection.Connection)
                {
                    DatabaseConnection.Connection.Execute(
                        @"UPDATE FixtureScore
                          SET Completed = @Completed
                          WHERE FixtureId = @FixtureId",
                        new
                        {
                            FixtureId = Id
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
}