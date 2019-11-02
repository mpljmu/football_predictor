using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FootballPredictor.Models.Competitions;
using FootballPredictor.Models.Connections;
using Dapper;

namespace FootballPredictor.Repositories.Seasons
{
    public class SeasonRepository : BaseRepository, ISeasonRepository
    {
        public SeasonRepository(IDatabaseConnection databaseConnection)
            : base(databaseConnection)
        {
        }


        public IEnumerable<ISeason> Get(IEnumerable<int> ids)
        {
            try
            {
                using (var connection = DatabaseConnection.NewConnection())
                {
                    var seasons = connection.Query<Season>(
                        @"SELECT DISTINCT
                            Id Id,
                            [Name] [Name],
                            [StartDate],
                            [EndDate]
                          FROM
                            Season
                          WHERE
                            Id IN @ids
                          ORDER BY
                            [StartDate] DESC",
                        new
                        {
                            ids
                        }
                    );
                    return seasons;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public IEnumerable<ISeason> GetByCompetitionId(int id)
        {
            try
            {
                using (var connection = DatabaseConnection.NewConnection())
                {
                    var ids = connection.Query<int>(
                        @"SELECT
                            SeasonId Id
                          FROM
                            CompetitionSeason
                          WHERE
                            CompetitionId = @id",
                        new
                        {
                            id
                        }
                    );
                    var seasons = Get(ids);
                    return seasons;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}