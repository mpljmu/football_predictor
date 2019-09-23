using System;
using FootballPredictor.Models.Clubs;
using FootballPredictor.Models.Connections;

namespace FootballPredictor.Models.Fixtures
{
    public interface IFixture
    {
        IClub AwayClub { get; }
        bool Completed { get; }
        DateTime Date { get; }
        FixtureScore Score { get;  }
        IClub HomeClub { get; }
        int Id { get; }
        bool OpenForPredictions { get; }
    }
}