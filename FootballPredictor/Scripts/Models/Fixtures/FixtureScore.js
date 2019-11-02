var Models;
(function (Models) {
    var Fixtures;
    (function (Fixtures) {
        var FixtureScore = /** @class */ (function () {
            function FixtureScore(id, homeGoals, awayGoals, completed) {
                this.id = id;
                this.homeGoals = homeGoals;
                this.awayGoals = awayGoals;
                this.completed = completed;
            }
            return FixtureScore;
        }());
        Fixtures.FixtureScore = FixtureScore;
    })(Fixtures = Models.Fixtures || (Models.Fixtures = {}));
})(Models || (Models = {}));
