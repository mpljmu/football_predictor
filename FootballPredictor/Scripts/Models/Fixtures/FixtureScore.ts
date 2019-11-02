namespace Models.Fixtures {
    export class FixtureScore {
        id: number;
        homeGoals: number;
        awayGoals: number;
        completed: boolean;

        constructor(id: number, homeGoals: number, awayGoals: number, completed: boolean) {
            this.id = id;
            this.homeGoals = homeGoals;
            this.awayGoals = awayGoals;
            this.completed = completed;
        }
    }
}