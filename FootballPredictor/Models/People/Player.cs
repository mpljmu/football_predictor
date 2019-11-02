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


        public Player(int id)
        {
            Id = id;
        }
        public Player(int id, IUser user)
        {
            Id = id;
            User = user;
        }
        public Player(int id, IUser user, IEnumerable<IPrediction> predictions)
        {
            Id = id;
            User = user;
            Predictions = predictions;
        }



        public IEnumerable<IOpenPrediction> GetOpenPredictions() {
            try
            {
                if (Predictions == null)
                {
                    throw new Exception("Predictions property of player not set");
                }
                var openPredictions = new List<IOpenPrediction>();
                foreach (var prediction in Predictions)
                {
                    if (prediction.Fixture.OpenForPredictions)
                    {
                        openPredictions.Add((OpenPrediction)prediction);
                    }
                }
                return openPredictions;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<IClosedPrediction> GetClosedPredictions()
        {
            try
            {
                if (Predictions == null)
                {
                    throw new Exception("Predictions property of player not set");
                }
                var closedPredictions = new List<IClosedPrediction>();
                foreach (var prediction in Predictions)
                {
                    if (!prediction.Fixture.OpenForPredictions)
                    {
                        closedPredictions.Add(
                            (IClosedPrediction)prediction
                        );
                    }
                }
                return closedPredictions;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<IClosedPrediction> GetLiveClosedPredictions()
        {
            try
            {
                var closedPredictions = GetClosedPredictions();
                var predictions = new List<IClosedPrediction>();
                foreach (var prediction in closedPredictions)
                {
                    if (!prediction.Fixture.Completed)
                    {
                        predictions.Add(prediction);
                    }
                }
                return predictions;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<IClosedPrediction> GetCompletedClosedPredictions()
        {
            try
            {
                var closedPredictions = GetClosedPredictions();
                var predictions = new List<IClosedPrediction>();
                foreach (var prediction in closedPredictions)
                {
                    if (prediction.Fixture.Completed)
                    {
                        predictions.Add(prediction);
                    }
                }
                return predictions;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        //public IEnumerable<IPrediction> Form
        //{
        //    get
        //    {
        //        if (ClosedPredictions == null)
        //        {
        //            return null;
        //        }
        //        var form = new List<IPrediction>();
        //        var closedPredictions = ClosedPredictions.ToList();
        //        int countOfClosePredictions = closedPredictions.Count;
        //        for (var i = countOfClosePredictions - 1; i < countOfClosePredictions - 10; countOfClosePredictions--)
        //        {
        //            if (i == -1)
        //            {
        //                break;
        //            }
        //            else
        //            {
        //                form.Add(closedPredictions[i]);
        //            }
        //        }
        //        return form;
        //    }
        //}
        //public int CompletedCorrectScores
        //{
        //    get
        //    {
        //        if (ClosedPredictions == null) {
        //            return 0;
        //        }
        //        int count = 0;
        //        foreach (var prediction in ClosedPredictions)
        //        {
        //            if (prediction.Fixture.Completed)
        //            {
        //                if (CompetitionSeason.CalculatePredictionPoints(prediction) == CompetitionSeason.PointsFor(PredictionOutcome.CorrectScore))
        //                {
        //                    count++;
        //                }
        //            }
        //        }
        //        return count;
        //    }
        //}
        //public int CompletedCorrectOutcomes
        //{
        //    get
        //    {
        //        if (ClosedPredictions == null)
        //        {
        //            return 0;
        //        }
        //        int count = 0;
        //        foreach (var prediction in ClosedPredictions)
        //        {
        //            if (prediction.Fixture.Completed)
        //            {
        //                if (CompetitionSeason.CalculatePredictionPoints(prediction) == CompetitionSeason.PointsFor(PredictionOutcome.CorrectOutcome))
        //                {
        //                    count++;
        //                }
        //            }
        //        }
        //        return count;
        //    }
        //}
        //public int CompletedIncorrectOutcomes
        //{
        //    get
        //    {
        //        if (ClosedPredictions == null)
        //        {
        //            return 0;
        //        }
        //        int count = 0;
        //        foreach (var prediction in ClosedPredictions)
        //        {
        //            if (prediction.Fixture.Completed)
        //            {
        //                if (CompetitionSeason.CalculatePredictionPoints(prediction) == CompetitionSeason.PointsFor(PredictionOutcome.IncorrectOutcome))
        //                {
        //                    count++;
        //                }
        //            }
        //        }
        //        return count;
        //    }
        //}
        //public int CompletedMissedPredictions
        //{
        //    get
        //    {
        //        // TODO: Might be a calculation based on above
        //        return 0;
        //    }
        //}
        //public int CompletedTotalPoints
        //{
        //    get
        //    {
        //        if (ClosedPredictions == null)
        //        {
        //            return 0;
        //        }
        //        return CompletedCorrectScores * CompetitionSeason.PointsFor(PredictionOutcome.CorrectScore)
        //            + CompletedCorrectOutcomes * CompetitionSeason.PointsFor(PredictionOutcome.CorrectOutcome)
        //            + CompletedIncorrectOutcomes * CompetitionSeason.PointsFor(PredictionOutcome.IncorrectOutcome);
        //    }
        //}
        //public int CompletedTotalPredictions {
        //    get
        //    {
        //        if (ClosedPredictions == null)
        //        {
        //            return 0;
        //        }
        //        return CompletedCorrectOutcomes + CompletedCorrectScores + CompletedIncorrectOutcomes;
        //    }
        //}
    }
}