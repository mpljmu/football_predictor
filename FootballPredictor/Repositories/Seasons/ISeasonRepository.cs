using System.Collections.Generic;
using FootballPredictor.Models.Competitions;

namespace FootballPredictor.Repositories.Seasons
{
    public interface ISeasonRepository
    {
        IEnumerable<ISeason> Get(IEnumerable<int> ids);
        IEnumerable<ISeason> GetByCompetitionId(int id);
    }
}