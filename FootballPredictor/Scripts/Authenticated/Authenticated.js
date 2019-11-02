(function () {
    /* Get a competition and the current season */
    var selectCompetition = $("#selectedCompetition");
    var selectSeason = $("#selectedSeason");
    Models.Competitions.Competition.getCompetitionsByUserId(3).done(function (competitions) {
        for (var i in competitions) {
            selectCompetition.append("<option value=\"" + competitions[i].id + "\">" + competitions[i].name + "</option>");
        }
        /* Get the seasons the user has been active for in the chosen competition */
        setCompetitionSeasons(selectCompetition.val());
    });
    selectCompetition.change(function () {
        setCompetitionSeasons($(this).val());
    });
    function setCompetitionSeasons(competitionId) {
        selectSeason.empty();
        Models.Seasons.Season.getByCompetitionId(competitionId).done(function (seasons) {
            for (var i in seasons) {
                selectSeason.append("<option value=\"" + seasons[i].id + "\" data-start-date=\"" + seasons[i].startDate + "\" data-end-date=\"" + seasons[i].endDate + "\">" + seasons[i].name + "</option>");
            }
        });
    }
    function setRankings() { }
    function getFixtures() { }
    function getLivePredictions() { }
    ;
})();
