using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FootballPredictor.Models.People;

namespace FootballPredictor.Models.Competitions
{
    public class Player : IPlayer
    {
        public int Id { get; set; }
        public IUser User { get; private set; }
        public ICompetitionSeason CompetitionSeason { get; private set; }
        public int Points
        {
            get
            {
                int totalPoints = 0;
                foreach (Prediction prediction in Predictions)
                {
                    totalPoints += prediction.Points;
                }
                return totalPoints;
            }
        }
        public IEnumerable<Prediction> Predictions { get; private set; }
        public ICompetition Competition => throw new NotImplementedException();
        public ISeason Season => throw new NotImplementedException();
        private IPrediction[] Form { get; }
        IEnumerable<IPrediction> IPlayer.Predictions { get; }


        public Player(int id)
        {
            Id = id;
        }
    }
}