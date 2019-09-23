namespace FootballPredictor.Models.Rankings
{
    public interface IRanking
    {
        int CorrectOutcomes { get; }
        int CorrectScores { get; }
        int MissedPredictions { get; }
        string PlayerName { get; }
        int Position { get; set; }
        int Predictions { get; }
        int TotalPoints { get; }
    }
}