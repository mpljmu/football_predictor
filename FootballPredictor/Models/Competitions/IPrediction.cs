using FootballPredictor.Models.Connections;
using FootballPredictor.Models.Loggers;
using FootballPredictor.Models.People;

namespace FootballPredictor.Models.Competitions
{
    public interface IPrediction
    {
        int AwayGoals { get; }
        IDatabaseConnection DatabaseConnection { set; }
        Fixture Fixture { get; }
        int HomeGoals { get; }
        int Id { get; }
        ILogger Logger { get; set; }
        int Points { get; }
        User User { get; }

        IPrediction GetFromDatabase();
        void Insert();
        void UpdateScore(int homeGoals, int awayGoals, Player player);
    }
}