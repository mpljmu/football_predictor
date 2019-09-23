using FootballPredictor.Models.Competitions;
using FootballPredictor.Models.Connections;
using FootballPredictor.Models.Fixtures;
using FootballPredictor.Models.Loggers;
using FootballPredictor.Models.People;

namespace FootballPredictor.Models.Predictions
{
    public interface IPrediction
    {
        IFixture Fixture { get; set; }
        int Id { get; set; }
        IPlayer Player { get; set; }
        IPredictionScore Score { get; set; }

    }
}