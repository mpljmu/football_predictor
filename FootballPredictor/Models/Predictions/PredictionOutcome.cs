using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPredictor.Models.Predictions
{
    // An enumeration of the outcomes, not the points for each outcome.
    // This is separate for each CompetitionSeason
    public enum PredictionOutcome
    {
        CorrectScore,
        CorrectOutcome,
        IncorrectOutcome,
        NoFixtureScore
    }
}