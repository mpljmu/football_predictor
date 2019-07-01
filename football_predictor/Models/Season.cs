using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace football_predictor.Models
{
    public class Season
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}