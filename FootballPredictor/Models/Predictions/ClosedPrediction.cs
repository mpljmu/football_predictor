using FootballPredictor.Models.Fixtures;
using FootballPredictor.Models.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPredictor.Models.Predictions
{
    public class ClosedPrediction : Prediction, IClosedPrediction
    {
        public PredictionOutcome Outcome {
            get
            {
                // Check as at first there may be no fixture score record
                if (Fixture.Score == null)
                {
                    return PredictionOutcome.NoFixtureScore;
                }
                if (Fixture.Score.HomeGoals == Score.HomeGoals && Fixture.Score.AwayGoals == Score.AwayGoals)
                {
                    return PredictionOutcome.CorrectScore;
                }
                else if (Fixture.Score.HomeGoals == Fixture.Score.AwayGoals && Score.HomeGoals == Score.AwayGoals)
                {
                    // Correctly guessed a draw
                    return PredictionOutcome.CorrectOutcome;
                }
                else if (
                    (Fixture.Score.HomeGoals > Fixture.Score.AwayGoals && Score.HomeGoals > Score.AwayGoals)
                    || (Fixture.Score.AwayGoals > Fixture.Score.HomeGoals && Score.AwayGoals > Score.HomeGoals)
                )
                {
                    return PredictionOutcome.CorrectOutcome;
                }
                else
                {
                    return PredictionOutcome.IncorrectOutcome;
                }
            }
        }

        public ClosedPrediction(int id, IPlayer player, IFixture fixture, IPredictionScore predictionScore)
            : base(id, player, fixture, predictionScore)
        {

        }

    }
}