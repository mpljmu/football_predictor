using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using FootballPredictor.App_Start;
using FootballPredictor.Models.Clubs;
using FootballPredictor.Models.Connections;
using FootballPredictor.Models.Fixtures;
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
                return 3;
            }
        }
        private int IncorrectOutcomePoints
        {
            get
            {
                return 0;
            }
        }
        private int NoFixtureScore {
            get
            {
                return 0;
            }
        }
        public IEnumerable<IFixture> FixturesOpenForPrediction
        {
            get
            {
                if (Fixtures == null)
                {
                    return null;
                }
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
                if (Fixtures == null)
                {
                    return null;
                }
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
                if (Fixtures == null)
                {
                    return null;
                }
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


        public CompetitionSeason(int id)
        {
            Id = id;
        }
        public CompetitionSeason(int id, Competition competition, Season season)
        {
            Id = id;
            Competition = competition;
            Season = season;
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
                case PredictionOutcome.NoFixtureScore:
                    return NoFixtureScore;
                default:
                    return 0;
            }
        }
        public int CalculatePredictionPoints(IClosedPrediction prediction)
        {
            // Check as at first there may be no fixture score record
            if (prediction.Fixture.Score == null)
            {
                return 0;
            }
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
                || (prediction.Fixture.Score.AwayGoals > prediction.Fixture.Score.HomeGoals && prediction.Score.AwayGoals > prediction.Score.HomeGoals)
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