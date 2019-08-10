using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;

namespace FootballPredictor.Models.Competitions
{
    public enum Points
    {

    }
    public class Competition : ICompetition
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
    }
}