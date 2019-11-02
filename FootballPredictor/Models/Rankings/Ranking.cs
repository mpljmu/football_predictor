using FootballPredictor.Models.Competitions;
using FootballPredictor.Models.People;
using FootballPredictor.Models.Predictions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPredictor.Models.Rankings
{
    public class Ranking : IRanking
    {

        public int Position { get; set; }
        public string PlayerName { get; private set; }
        public int Predictions { get; private set; }
        public int CorrectScores { get; private set; }
        public int CorrectOutcomes { get; private set; }
        public int IncorrectOutcomes { get; private set; }
        public int MissedPredictions { get; private set; }
        public int TotalPoints { get; private set; }


        public Ranking(string playerName, IEnumerable<IClosedPrediction> predictions, ICompetitionSeason competitionSeason)
        {
            PlayerName = playerName;
            Predictions = predictions.Count();
            foreach (var prediction in predictions)
            {
                if (prediction.Outcome == PredictionOutcome.CorrectScore)
                {
                    CorrectScores++;
                } else if (prediction.Outcome == PredictionOutcome.CorrectOutcome)
                {
                    CorrectOutcomes++;
                } else if (prediction.Outcome == PredictionOutcome.IncorrectOutcome)
                {
                    IncorrectOutcomes++;
                } else if (prediction.Outcome == PredictionOutcome.NoFixtureScore)
                {
                    // Nothing to append to
                }
            }
            TotalPoints = competitionSeason.PointsFor(PredictionOutcome.CorrectScore) * CorrectScores;
            TotalPoints += competitionSeason.PointsFor(PredictionOutcome.CorrectOutcome) * CorrectOutcomes;
            TotalPoints += competitionSeason.PointsFor(PredictionOutcome.IncorrectOutcome) * IncorrectOutcomes;
        }


        public static IEnumerable<IRanking> OrderRankings(IEnumerable<IRanking> rankings)
        {
            var orderedRankings = rankings
                .OrderByDescending(ranking => ranking.TotalPoints)
                .ThenByDescending(ranking => ranking.CorrectOutcomes).ToList();
            for (int i = 0; i < orderedRankings.Count; i++)
            {
                // Players on the same points and same correct scores share a position
                if (i > 0 && orderedRankings[i].TotalPoints == orderedRankings[i - 1].TotalPoints)
                {
                    // Same total points as player above, check for who has the most correct scores
                    if (orderedRankings[i].CorrectScores > orderedRankings[i - 1].CorrectScores)
                    {
                        orderedRankings[i].Position = i;
                        orderedRankings[i - 1].Position = i + 1;
                    } else if (orderedRankings[i].CorrectScores < orderedRankings[i - 1].CorrectScores)
                    {
                        orderedRankings[i].Position = i + 1;
                        orderedRankings[i - 1].Position = i;
                    } else
                    {
                        orderedRankings[i].Position = i;
                        orderedRankings[i - 1].Position = i;
                    }
                } else
                {
                    // Different total points count to higher ranked player, nothing to compare
                    orderedRankings[i].Position = i + 1;
                }
            }
            // Players on the same points and same correct scores share a position
            return orderedRankings;
        }
    }
}