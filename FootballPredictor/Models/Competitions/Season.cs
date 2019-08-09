using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPredictor.Models.Competitions
{
    public class Season : ISeason
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        private DateTime StartDate { get; set; }
        private DateTime EndDate { get; set; }
    }
}