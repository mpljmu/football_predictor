using FootballPredictor.Models.Competitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using Dapper.Contrib.Extensions;
using FootballPredictor.Models.Connections;

namespace FootballPredictor.Repositories.Competitions
{
    public class CompetitionRepository : BaseRepository, ICompetitionRepository
    {
        public CompetitionRepository(IDatabaseConnection databaseConnection)
            : base(databaseConnection)
        {

        }

        public IEnumerable<ICompetition> Get(IEnumerable<int> ids)
        {
            try
            {
                using (var connection = DatabaseConnection.NewConnection())
                {
                    var competitions = connection.Query<Competition>(
                        @"SELECT
                            Id,
                            Name
                          FROM
                            Competition
                          WHERE
                            Id IN @ids
                          ORDER BY
                            Name",
                        new
                        {
                            ids
                        }
                    );
                    return competitions;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ICompetition> GetByUserId(int id)
        {
            try
            {
                using (var connection = DatabaseConnection.NewConnection())
                {
                    var competitionIds = connection.Query<int>(
                        @"SELECT DISTINCT
                            CompetitionId
                          FROM
                            vwPlayerCompetitionSeasons
                          WHERE
                            UserId = @id",
                        new
                        {
                            id
                        }
                    );
                    var competitions = Get(competitionIds);
                    return competitions;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}