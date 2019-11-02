var Models;
(function (Models) {
    var Seasons;
    (function (Seasons) {
        var Season = /** @class */ (function () {
            function Season(id, name, startDate, endDate) {
                this.id = id;
                this.name = name;
                this.startDate = startDate;
                this.endDate = endDate;
            }
            Season.getByCompetitionId = function (id) {
                return ($.get(Models.Applications.Application.root + "/api/competitions/" + id + "/seasons").then(function (data) {
                    var seasons = new Array();
                    for (var i in data) {
                        seasons.push(new Models.Seasons.Season(data[i].Id, data[i].Name, data[i].StartDate, data[i].EndDate));
                    }
                    return seasons;
                }));
            };
            return Season;
        }());
        Seasons.Season = Season;
    })(Seasons = Models.Seasons || (Models.Seasons = {}));
})(Models || (Models = {}));
