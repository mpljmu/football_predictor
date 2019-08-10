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
        public ICompetition Competition { get; private set; }
        public ISeason Season { get; private set; }
        public IEnumerable<IPlayer> Players { get; private set; }
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
        [Inject]
        public IDatabaseConnection DatabaseConnection { private get; set; }


        public CompetitionSeason(int id)
        {
            Id = id;
            NinjectWebCommon.Bootstrapper.Kernel.Inject(this);
        }


        public IEnumerable<Fixture> GetFixtures()
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
        public int CalculatePredictionPoints(ClosedPrediction prediction)
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