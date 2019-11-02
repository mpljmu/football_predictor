using System.Collections.Generic;
using FootballPredictor.Models.Competitions;

namespace FootballPredictor.Repositories.Competitions
{
    public interface ICompetitionRepository
    {
        IEnumerable<ICompetition> Get(IEnumerable<int> ids);
        IEnumerable<ICompetition> GetByUserId(int id);
    }
}