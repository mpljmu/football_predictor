namespace FootballPredictor.Models.Predictions
{
    public interface IClosedPrediction : IPrediction
    {
        PredictionOutcome Outcome { get; }
    }
}