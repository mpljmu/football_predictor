using FootballPredictor.Models.Connections;

namespace FootballPredictor.Models.Clubs
{
    public interface IClub
    {
        IDatabaseConnection DatabaseConnection { get; set; }
        int Id { get; }
        string Name { get; }
    }
}