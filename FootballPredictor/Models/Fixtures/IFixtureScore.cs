using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballPredictor.Models.Fixtures
{
    public interface IFixtureScore
    {
        byte HomeGoals { get; }
        byte AwayGoals { get; }
    }
}
