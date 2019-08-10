using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FootballPredictor.Models.People;
using FootballPredictor.Models.Predictions;

namespace FootballPredictor.Models.Competitions
{
    public class Player : IPlayer
    {
        public int Id { get; set; }
        public IUser User { get; private set; }
        public ICompetitionSeason CompetitionSeason { get; private set; }
        public IEnumerable<OpenPrediction> OpenPredictions { get; private set; }
        public IEnumerable<ClosedPrediction> ClosedPredictions { get; private set; }
        private IPrediction[] Form { get; }
        public int CorrectScores {
            get
            {
                int count = 0;
                foreach (var prediction in ClosedPredictions)
                {
                    if (CompetitionSeason.CalculatePredictionPoints(prediction) == CompetitionSeason.PointsFor(PredictionOutcome.CorrectScore))
                    {
                        count++;
                    }
                }
                return count;
            }
        }
        public int CorrectOutcomes
        {
            get
            {
                int count = 0;
                foreach (var prediction in ClosedPredictions)
                {
                    if (CompetitionSeason.CalculatePredictionPoints(prediction) == CompetitionSeason.PointsFor(PredictionOutcome.CorrectOutcome))
                    {
                        count++;
                    }
                }
                return count;
            }
        }
        public int IncorrectOutcomes
        {
            get
            {
                int count = 0;
                foreach (var prediction in ClosedPredictions)
                {
                    if (CompetitionSeason.CalculatePredictionPoints(prediction) == CompetitionSeason.PointsFor(PredictionOutcome.IncorrectOutcome))
                    {
                        count++;
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
        public int TotalPoints
        {
            get
            {
                int totalPoints = 0;
                foreach (var prediction in ClosedPredictions)
                {
                    totalPoints += CompetitionSeason.CalculatePredictionPoints(prediction);
                }
                return totalPoints;
            }
        }


        public Player(int id)
        {
            Id = id;
        }
    }
}