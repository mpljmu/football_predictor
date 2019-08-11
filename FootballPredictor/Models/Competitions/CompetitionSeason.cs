using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using FootballPredictor.App_Start;
using FootballPredictor.Models.Clubs;
using FootballPredictor.Models.Connections;
using FootballPredictor.Models.People;
using FootballPredictor.Models.Predictions;
using Ninject;

namespace FootballPredictor.Models.Competitions
{
    public class CompetitionSeason : ICompetitionSeason
    {
        public int Id { get; set; }
        public ICompetition Competition { get; protected set; }
        public ISeason Season { get; protected set; }
        public IEnumerable<IPlayer> Players { get; protected set; }
        public IEnumerable<IFixture> Fixtures { get; protected set; }
        private int CorrectOutcomePoints
        {
            get
            {
                return 1;
            }
        }
        private int CorrectScorePoints
        {
            get
            {
                return 1;
            }
        }
        private int IncorrectOutcomePoints
        {
            get
            {
                return 0;
            }
        }
        public IEnumerable<IFixture> FixturesOpenForPrediction
        {
            get
            {
                var fixtures = new List<IFixture>();
                foreach (IFixture fixture in Fixtures)
                {
                    if (fixture.OpenForPredictions)
                    {
                        fixtures.Add(fixture);
                    }
                }
                return fixtures;
            }
        }
        public IEnumerable<IFixture> LiveFixtures
        {
            get
            {
                var fixtures = new List<IFixture>();
                foreach (var fixture in Fixtures)
                {
                    if (!fixture.OpenForPredictions && !fixture.Completed)
                    {
                        fixtures.Add(fixture);
                    }
                }
                return fixtures;
            }
        }
        public IEnumerable<IFixture> CompletedFixtures
        {
            get
            {
                var fixtures = new List<IFixture>();
                foreach (var fixture in Fixtures)
                {
                    if (fixture.Completed)
                    {
                        fixtures.Add(fixture);
                    }
                }
                return fixtures;
            }
        }
        [Inject]
        public IDatabaseConnection DatabaseConnection { protected get; set; }


        public CompetitionSeason(int id)
        {
            Id = id;
            NinjectWebCommon.Bootstrapper.Kernel.Inject(this);
        }


        public void GetFixtures()
        {
            try
            {
                using (DatabaseConnection.Connection)
                {
                    Fixtures = DatabaseConnection.Connection.Query<Fixture, FixtureScore, Club, Club, Fixture>(
                    (@"SELECT
	                    Fixture.Id,
	                    Fixture.HomeClubId,
	                    Fixture.AwayClubId,
	                    Fixture.[Date],
	                    FixtureScore.HomeGoals,
	                    FixtureScore.AwayGoals,
	                    FixtureScore.Completed
                      FROM
		                    CompetitionSeason
	                    INNER JOIN CompetitionSeasonClub
		                    ON CompetitionSeason.Id = CompetitionSeasonClub.CompetitionSeasonId
	                    INNER JOIN Fixture
		                    ON (Fixture.HomeClubId = CompetitionSeasonClub.ClubId OR Fixture.AwayClubId = CompetitionSeasonClub.ClubId)
	                    LEFT OUTER JOIN FixtureScore
		                    ON Fixture.Id = FixtureScore.FixtureId
                      WHERE
                        CompetitionSeason.Id = @CompetitionSeasonId"
                    ),
                    (fixture, fixtureScore, homeClub, awayClub) =>
                    {
                        // CHECK WHY THE SCORE OBJECT IS NULL - IS IT THE QUERY??
                        fixture = new Fixture(fixture.Id, homeClub, awayClub, fixture.Date, fixtureScore);
                        return fixture;
                    },
                    new
                    {
                        CompetitionSeasonId = Id
                    },
                    splitOn: "Id,HomeGoals,Id,Id").ToList();

                }
            }
            catch (Exception ex)
            {
                // TODO
                throw ex;
            }
        }
        public void GetPlayers()
        {
            using (DatabaseConnection.Connection)
            {
                try
                {
                    Players = DatabaseConnection.Connection.Query<Player, User, Player>(
                        (@"SELECT
	                        PlayerId Id,
	                        [User].Id UserId
                          FROM
		                        vwPlayers
	                        INNER JOIN Player
		                        ON vwPlayers.PlayerId = Player.Id
	                        INNER JOIN [User]
		                        ON vwPlayers.UserId = [User].Id
                          WHERE
                            CompetitionSeasonId = @CompetitionSeasonId"
                        ),
                        (thisPlayer, thisUser) => {
                            thisPlayer.User = thisUser;
                            return thisPlayer;
                        },
                        new
                        {
                            CompetitionSeasonId = Id,
                        }
                    );
                }
                catch (Exception ex)
                {
                    // TODO: Log
                    throw ex;
                }
            }
        }
        public void AddPlayer(int playerId)
        {
            // TO DO
        }
        public int PointsFor(PredictionOutcome predictionOutcome)
        {
            switch (predictionOutcome)
            {
                case PredictionOutcome.CorrectScore:
                    return CorrectScorePoints;
                case PredictionOutcome.CorrectOutcome:
                    return CorrectOutcomePoints;
                case PredictionOutcome.IncorrectOutcome:
                    return IncorrectOutcomePoints;
                default:
                    return 0;
            }
        }
        public int CalculatePredictionPoints(Prediction prediction)
        {
            if (prediction.Fixture.Score.HomeGoals == prediction.Score.HomeGoals && prediction.Fixture.Score.AwayGoals == prediction.Score.AwayGoals)
            {
                return PointsFor(PredictionOutcome.CorrectScore);
            }
            else if (prediction.Fixture.Score.HomeGoals == prediction.Fixture.Score.AwayGoals && prediction.Score.HomeGoals == prediction.Score.AwayGoals)
            {
                // Correctly guessed a draw
                return PointsFor(PredictionOutcome.CorrectOutcome);
            }
            else if (
                (prediction.Fixture.Score.HomeGoals > prediction.Fixture.Score.AwayGoals && prediction.Score.HomeGoals > prediction.Score.AwayGoals)
                || (prediction.Fixture.Score.AwayGoals < prediction.Fixture.Score.HomeGoals && prediction.Score.AwayGoals < prediction.Score.HomeGoals)
            )
            {
                return PointsFor(PredictionOutcome.CorrectOutcome);
            }
            else
            {
                return PointsFor(PredictionOutcome.IncorrectOutcome);
            }
        }
    }
}