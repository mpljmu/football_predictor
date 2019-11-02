namespace Models.Seasons {
    export class Season {
        id: number;
        name: string;
        startDate: Date;
        endDate: Date;

        public constructor(id: number, name: string, startDate: Date, endDate: Date) {
            this.id = id;
            this.name = name;
            this.startDate = startDate;
            this.endDate = endDate;
        }


        public static getByCompetitionId(id: number) : JQueryPromise<Array<Models.Seasons.Season>> {
            return (
                $.get(
                    `${Models.Applications.Application.root}/api/competitions/${id}/seasons`
                ).then((data) => {
                    let seasons = new Array<Models.Seasons.Season>();
                    for (let i in data) {
                        seasons.push(
                            new Models.Seasons.Season(data[i].Id, data[i].Name, data[i].StartDate, data[i].EndDate)
                        )
                    }
                    return seasons;
                })
            );
        }
    }
}