namespace Models.CompetitionSeason {
    export class CompetitionSeason {
        id: number;
        competition: Models.Competitions.Competition;
        season: any;


        constructor(id: number, competition: Models.Competitions.Competition, season: any) {
            this.id = id;
            this.competition = competition;
            this.season = season;
        }
    }
}
