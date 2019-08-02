using System.Data;

namespace FootballPredictor.Models.Connections
{
    public interface IDatabaseConnection
    {
        IDbCommand Command { get; }
        IDbConnection Connection { get; }
    }
}