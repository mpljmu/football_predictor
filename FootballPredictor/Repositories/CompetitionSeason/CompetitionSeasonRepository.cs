using FootballPredictor.Models.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using FootballPredictor.Models.Competitions;

namespace FootballPredictor.Repositories.CompetitionSeason
{
    public class CompetitionSeasonRepository : ICompetitionSeasonRepository
    {
        private IDatabaseConnection DatabaseConnection { get; set; }


        public CompetitionSeasonRepository(IDatabaseConnection databaseConnection)
        {
            DatabaseConnection = databaseConnection;
        }


        public ICompetitionSeason Get(int competitionSeasonId)
        {
            try
            {
                using (var connection = DatabaseConnection.NewConnection())
                {
                    var competitionSeason = connection.Query<Models.Competitions.CompetitionSeason, Competition, Season, Models.Competitions.CompetitionSeason>(
                        @"SELECT
                            CompetitionSeasonId Id,
                            CompetitionId Id,
                            CompetitionName Name,
                            SeasonId Id,
                            SeasonName Name,
                            SeasonStartDate StartDate,
                            SeasonEndDate EndDate
                          FROM
                            vwCompetitionSeasons
                          WHERE
                            CompetitionSeasonId = @CompetitionSeasonId",
                        (thisCompetitionSeason, competition, season) =>
                        {
                            thisCompetitionSeason = new Models.Competitions.CompetitionSeason(
                                thisCompetitionSeason.Id,
                                competition,
                                season
                            );
                            return thisCompetitionSeason;
                        },
                        new
                        {
                            CompetitionSeasonId = competitionSeasonId
                        }
                    ).FirstOrDefault();
                    return competitionSeason;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}