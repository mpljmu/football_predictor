using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FootballPredictor.App_Start;
using FootballPredictor.Models.Connections;
using Ninject;

namespace FootballPredictor.Models.Clubs
{
    public class Club : IClub
    {
        public int Id { get; private set; }
        public string Name { get; private set; }


        public Club(int id, string name)
        {
            Id = id;
            Name = name;
        }

    }
}