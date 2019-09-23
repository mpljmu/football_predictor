namespace FootballPredictor.Models.People
{
    public interface IUser
    {
        bool Active { get; set; }
        string FullName { get; }
        int Id { get; }
        bool IsAdministrator { get; set; }
        IPassword Password { set; }
        string Username { get; }
    }
}