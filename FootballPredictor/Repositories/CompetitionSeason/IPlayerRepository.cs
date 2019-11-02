using System.Collections.Generic;
using FootballPredictor.Models.Competitions;
using FootballPredictor.Models.People;
using FootballPredictor.Models.Predictions;

namespace FootballPredictor.Repositories.CompetitionSeason
{
    public interface IPlayerRepository
    {
        IPlayer Get(int id);
        void Add(int id, IPlayer player);
        void Delete(int id, int playerId);
        IEnumerable<IPlayer> GetAllByCompetitionSeason(int competitionSeasonId);
    }
}