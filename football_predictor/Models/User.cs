using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace football_predictor.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        private string Hash { get; set; }
        
    }
}