var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
var Models;
(function (Models) {
    var CompetitionSeason;
    (function (CompetitionSeason) {
        var CompetitionSeasonFixture = /** @class */ (function (_super) {
            __extends(CompetitionSeasonFixture, _super);
            function CompetitionSeasonFixture() {
                return _super !== null && _super.apply(this, arguments) || this;
            }
            return CompetitionSeasonFixture;
        }(Models.Fixtures.Fixture));
        CompetitionSeason.CompetitionSeasonFixture = CompetitionSeasonFixture;
    })(CompetitionSeason = Models.CompetitionSeason || (Models.CompetitionSeason = {}));
})(Models || (Models = {}));
