using System;
using FootballPredictor.Models.Clubs;
using FootballPredictor.Models.Connections;

namespace FootballPredictor.Models.Competitions
{
    public interface IFixture
    {
        IClub AwayClub { get; set; }
        IDatabaseConnection DatabaseConnection { set; }
        DateTime Date { get; }
        FixtureScore Score { get; }
        IClub HomeClub { get; }
        int Id { get; }
        bool OpenForPredictions { get; }


        IFixture GetFromDatabase();
        void UpdateDate();
        void UpdateScore();
    }
}