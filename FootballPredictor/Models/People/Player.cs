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
using FootballPredictor.Models.Competitions;

namespace FootballPredictor.Models.People
{
    public class Player : IPlayer
    {
        public int Id { get; set; }
        public IUser User { get; set; }
        public ICompetitionSeason CompetitionSeason { get; set; }
        public IEnumerable<IPrediction> Predictions { get; set; }
        public IEnumerable<IOpenPrediction> OpenPredictions { get; }
        public IEnumerable<IPrediction> ClosedPredictions { get; }
        private IEnumerable<IPrediction> Form
        {
            get
            {
                if (ClosedPredictions == null)
                {
                    return null;
                }
                var form = new List<IPrediction>();
                var closedPredictions = ClosedPredictions.ToList();
                int countOfClosePredictions = closedPredictions.Count;
                for (var i = countOfClosePredictions - 1; i < countOfClosePredictions - 10; countOfClosePredictions--)
                {
                    if (i == -1)
                    {
                        break;
                    }
                    else
                    {
                        form.Add(closedPredictions[i]);
                    }
                }
                return form;
            }
        }
        public int CompletedCorrectScores
        {
            get
            {
                if (ClosedPredictions == null) {
                    return 0;
                }
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
                if (ClosedPredictions == null)
                {
                    return 0;
                }
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
                if (ClosedPredictions == null)
                {
                    return 0;
                }
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
        public int CompletedMissedPredictions
        {
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
                if (ClosedPredictions == null)
                {
                    return 0;
                }
                return CompletedCorrectScores * CompetitionSeason.PointsFor(PredictionOutcome.CorrectScore)
                    + CompletedCorrectOutcomes * CompetitionSeason.PointsFor(PredictionOutcome.CorrectOutcome)
                    + CompletedIncorrectOutcomes * CompetitionSeason.PointsFor(PredictionOutcome.IncorrectOutcome);
            }
        }
        public int CompletedTotalPredictions {
            get
            {
                if (ClosedPredictions == null)
                {
                    return 0;
                }
                return CompletedCorrectOutcomes + CompletedCorrectScores + CompletedIncorrectOutcomes;
            }
        }
        public IEnumerable<IPrediction> LiveClosedPrediction
        {
            get
            {
                if (ClosedPredictions == null)
                {
                    return null;
                }
                var predictions = new List<IPrediction>();
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


        public Player(int id)
        {
            Id = id;
        }
        public Player(int id, IUser user)
        {
            Id = id;
            User = user;
        }
    }
}