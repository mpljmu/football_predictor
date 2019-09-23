using FootballPredictor.Models.Connections;

namespace FootballPredictor.Models.Clubs
{
    public interface IClub
    {
        int Id { get; }
        string Name { get; }
    }
}