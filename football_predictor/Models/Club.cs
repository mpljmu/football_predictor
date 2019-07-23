using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPredictor.Models
{
    public class Club
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public Club()
        {

        }

        public Club(int id, string name)
        {
            Id = id;
            Name = name;
        }

    }
}