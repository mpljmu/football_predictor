using System.Collections.Generic;

namespace FootballPredictor.Models.Competitions
{
    public interface ICompetitionSeason
    {
        Competition Competition { get; }
        int Id { get; set; }
        Season Season { get; }

        void AddPlayer(int playerId);
        List<Fixture> GetFixtures();
        IEnumerable<Player> GetPlayers();
    }
}