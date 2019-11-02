using FootballPredictor.Models.Competitions;

namespace FootballPredictor.Repositories.CompetitionSeason
{
    public interface ICompetitionSeasonRepository
    {
        ICompetitionSeason Get(int competitionSeasonId);
    }
}