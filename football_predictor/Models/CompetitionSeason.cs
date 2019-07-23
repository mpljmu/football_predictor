using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;

namespace FootballPredictor.Models
{
    public class CompetitionSeason
    {
        private int _id;
        private Competition _competition;
        private Season _season;

        public CompetitionSeason(int id)
        {
            _id = id;
        }

        public List<Fixture> GetFixtures()
        {
            var fixtures = new List<Fixture>();
            try
            {
                using (var connection = new DatabaseConnection().Connection)
                {
                    connection.Open();
                    fixtures = connection.Query<Fixture, Club, Fixture>(
                    (@"SELECT fixture.fixtureID id, fixture.homeClubID id, homeClub.clubName name
                     FROM tblFixture fixture
                     INNER JOIN tblClub homeClub
                     ON fixture.homeClubID = homeClub.clubId"
                    ),
                    (fixture, club) =>
                    {
                        fixture.HomeClub = club;
                        return fixture;
                    },
                    splitOn: "id").ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return fixtures;
        }
    }
}