var Models;
(function (Models) {
    var Competitions;
    (function (Competitions) {
        var Competition = /** @class */ (function () {
            function Competition(id, name) {
                this.id = id;
                this.name = name;
            }
            Competition.getCompetitionsByUserId = function (id) {
                return ($.get(Models.Applications.Application.root + "/api/users/" + id + "/competitions").then(function (data) {
                    var competition = new Array();
                    for (var i in data) {
                        competition.push(new Models.Competitions.Competition(data[i].Id, data[i].Name));
                    }
                    return competition;
                }));
            };
            return Competition;
        }());
        Competitions.Competition = Competition;
    })(Competitions = Models.Competitions || (Models.Competitions = {}));
})(Models || (Models = {}));
