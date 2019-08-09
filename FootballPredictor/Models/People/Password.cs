using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPredictor.Models.People
{
    public class Password : IPassword
    {

        public string TextPassword { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }

    }
}