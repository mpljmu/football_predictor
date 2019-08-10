using FootballPredictor.Models.Competitions;
using FootballPredictor.Models.Connections;
using FootballPredictor.Models.Loggers;

namespace FootballPredictor.Models.Predictions
{
    public interface IPrediction
    {
        IDatabaseConnection DatabaseConnection { set; }
        IFixture Fixture { get; }
        int Id { get; }
        ILogger Logger { set; }
        IPlayer Player { get; }
        PredictionScore Score { get; }

        IPrediction GetFromDatabase();
    }
}