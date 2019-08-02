using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;

namespace FootballPredictor.Models.Competitions
{
    public class Competition
    {
        public int Id { get; private set; }
        private string Name { get; set; }

    }
}