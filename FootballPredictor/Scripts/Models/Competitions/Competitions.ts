namespace Models.Competitions {
    export class Competition {
        id: number;
        name: string;


        constructor(id: number, name: string) {
            this.id = id;
            this.name = name;
        }


        public static getCompetitionsByUserId(id: number) : JQueryPromise<Array<Models.Competitions.Competition>> {
            return (
                $.get(
                    `${Models.Applications.Application.root}/api/users/${id}/competitions`
                ).then((data) => {
                    let competition = new Array<Models.Competitions.Competition>();
                    for (let i in data) {
                        competition.push(
                            new Models.Competitions.Competition(data[i].Id, data[i].Name)
                        )
                    }
                    return competition;
                })
            );
        }
    }
}


