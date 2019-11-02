using FootballPredictor.Models.People;

namespace FootballPredictor.Repositories.Users
{
    public interface IUserRepository
    {
        IUser Get(int id);
        IUser GetByUsername(string username);
        IUser GetByAuthorisationToken(string token);
    }
}