using System.Data;

namespace FootballPredictor.Models.Connections
{
    public interface IDatabaseConnection
    {
        IDbConnection NewConnection();
    }
}