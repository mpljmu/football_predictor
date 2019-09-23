using FootballPredictor.Models.People;
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
        public int MissedPredictions { get; private set; }
        public int TotalPoints { get; private set; }


        public Ranking(string playerName, int predictions, int correctScores, int correctOutcomes, int missedPredictions, int totalPoints)
        {
            PlayerName = playerName;
            Predictions = predictions;
            CorrectScores = correctScores;
            CorrectOutcomes = correctOutcomes;
            MissedPredictions = missedPredictions;
            TotalPoints = totalPoints;
        }


        public static IEnumerable<IRanking> RankingsForCompletedClosedFixtures(IEnumerable<IPlayer> players)
        {
            try
            {
                var rankings = new List<IRanking>();
                foreach (var player in players)
                {
                    rankings.Add(
                        new Ranking(
                            player.User.FullName,
                            player.CompletedTotalPredictions,
                            player.CompletedCorrectScores,
                            player.CompletedCorrectOutcomes,
                            player.CompletedMissedPredictions,
                            player.CompletedTotalPoints
                        )
                    );
                }
                // Order the ranking based on total points then total correct scores
                var orderedRankings = rankings
                    .OrderBy(ranking => ranking.TotalPoints)
                    .ThenBy(ranking => ranking.CorrectOutcomes).ToList();
                return orderedRankings;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}