using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FootballPredictor.App_Start;
using FootballPredictor.Models.Connections;
using FootballPredictor.Models.People;
using FootballPredictor.Models.Predictions;
using Ninject;
using Dapper;

namespace FootballPredictor.Models.Competitions
{
    public class Player : IPlayer
    {
        public int Id { get; set; }
        public IUser User { get; set; }
        public ICompetitionSeason CompetitionSeason { get; private set; }
        public IEnumerable<OpenPrediction> OpenPredictions { get; private set; }
        public IEnumerable<Prediction> ClosedPredictions { get; private set; }
        private IEnumerable<IPrediction> Form {
            get
            {
                var form = new List<IPrediction>();
                var closedPredictions = ClosedPredictions.ToList();
                int countOfClosePredictions = closedPredictions.Count;
                for (var i = countOfClosePredictions - 1; i < countOfClosePredictions - 10; countOfClosePredictions--)
                {
                    if (i == -1)
                    {
                        break;
                    } else
                    {
                        form.Add(closedPredictions[i]);
                    }
                }
                return form;
            }
        }
        public int CompletedCorrectScores {
            get
            {
                int count = 0;
                foreach (var prediction in ClosedPredictions)
                {
                    if (prediction.Fixture.Completed)
                    {
                        if (CompetitionSeason.CalculatePredictionPoints(prediction) == CompetitionSeason.PointsFor(PredictionOutcome.CorrectScore))
                        {
                            count++;
                        }
                    }
                }
                return count;
            }
        }
        public int CompletedCorrectOutcomes
        {
            get
            {
                int count = 0;
                foreach (var prediction in ClosedPredictions)
                {
                    if (prediction.Fixture.Completed)
                    {
                        if (CompetitionSeason.CalculatePredictionPoints(prediction) == CompetitionSeason.PointsFor(PredictionOutcome.CorrectOutcome))
                        {
                            count++;
                        }
                    }
                }
                return count;
            }
        }
        public int CompletedIncorrectOutcomes
        {
            get
            {
                int count = 0;
                foreach (var prediction in ClosedPredictions)
                {
                    if (prediction.Fixture.Completed)
                    {
                        if (CompetitionSeason.CalculatePredictionPoints(prediction) == CompetitionSeason.PointsFor(PredictionOutcome.IncorrectOutcome))
                        {
                            count++;
                        }
                    }
                }
                return count;
            }
        }
        public int MissedPredictions {
            get
            {
                // TODO: Might be a calculation based on above
                return 0;
            }
        }
        public int CompletedTotalPoints
        {
            get
            {
                return CompletedCorrectScores * CompetitionSeason.PointsFor(PredictionOutcome.CorrectScore)
                    + CompletedCorrectOutcomes * CompetitionSeason.PointsFor(PredictionOutcome.CorrectOutcome)
                    + CompletedIncorrectOutcomes * CompetitionSeason.PointsFor(PredictionOutcome.IncorrectOutcome);
            }
        }
        public IEnumerable<Prediction> LiveClosedPrediction
        {
            get
            {
                var predictions = new List<Prediction>();
                foreach (var prediction in ClosedPredictions)
                {
                    if (!prediction.Fixture.Completed)
                    {
                        predictions.Add(prediction);
                    }
                }
                return predictions;
            }
        }
        [Inject]
        public IDatabaseConnection DatabaseConnection { get; set; }


        public Player(int id)
        {
            Id = id;
            NinjectWebCommon.Bootstrapper.Kernel.Inject(this);
        }


        public IPlayer GetFromDatabase()
        {
            try
            {
                using (DatabaseConnection.Connection)
                {
                    var player = DatabaseConnection.Connection.Query<Player, User, CompetitionSeason, Player>(
                     @"SELECT
	                    PlayerId,
	                    UserId,
	                    Username,
	                    Surname,
	                    Forename,
	                    CompetitionSeasonId
                      FROM
	                    vwPlayers
                      WHERE
	                    PlayerId = @Id",
                     (thisPlayer, thisUser, thisCompetitionSeason) =>
                     {
                         thisPlayer.User = thisUser;
                         thisPlayer.CompetitionSeason = thisCompetitionSeason;
                         return thisPlayer;
                     },
                     new
                     {
                         Id = Id
                     }
                    ).First();
                    return player;
                }
            }
            catch (Exception ex)
            {
                // TODO: Log
                throw ex;
            }
        }
        public void GetPredictions()
        {
            var predictions = DatabaseConnection.Connection.Query<OpenPrediction>(
                @"SELECT
	                Prediction.Id,
	                Prediction.HomeGoals,
	                Prediction.AwayGoals,
	                Prediction.SubmittedOn,
	                Prediction.PlayerId,
	                Fixture.Id FixtureId,
	                ISNULL(FixtureScore.Completed, 0) FixtureCompleted
                  FROM
                        Player
		            INNER JOIN Prediction
                        ON Player.Id = Prediction.PlayerId
	                INNER JOIN Fixture
		                ON Prediction.FixtureId = Fixture.Id
	                LEFT OUTER JOIN FixtureScore
		                ON Fixture.Id = FixtureScore.FixtureId
                  WHERE
                    Player.Id = @Id
                    ORDER BY
                    Fixture.Date DESC",
                new
                {
                    Id = Id
                }
            );
            var openPredictions = new List<OpenPrediction>();
            var closedPredictions = new List<Prediction>();
            foreach (var prediction in predictions)
            {
                if (prediction.Fixture.OpenForPredictions)
                {
                    openPredictions.Add(prediction);
                }
                else
                {
                    closedPredictions.Add(prediction);
                }
            }
            OpenPredictions = openPredictions;
            ClosedPredictions = closedPredictions;
        }
    }
}