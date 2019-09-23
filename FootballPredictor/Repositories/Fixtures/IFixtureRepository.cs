using FootballPredictor.Models.Fixtures;
using System;

namespace FootballPredictor.Repositories.Fixtures
{
    public interface IFixtureRepository
    {
        IFixture Get(int fixtureId);
        void Create(IFixture fixture);
        void Delete(int fixtureId);
        void Insert(IFixture fixture);
        void UpdateScore(int fixtureId, IFixtureScore sore);
        void Complete(int id);
        void Uncomplete(int id);
        bool IsCompleted(int id);
        void SetDate(int id, DateTime date);
    }
}