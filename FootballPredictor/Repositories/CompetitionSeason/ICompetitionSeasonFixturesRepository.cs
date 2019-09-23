using System.Collections.Generic;
using FootballPredictor.Models.Fixtures;

namespace FootballPredictor.Repositories.CompetitionSeason
{
    public interface ICompetitionSeasonFixturesRepository
    {
        IEnumerable<IFixture> Get(int competitionSeasonId);
    }
}