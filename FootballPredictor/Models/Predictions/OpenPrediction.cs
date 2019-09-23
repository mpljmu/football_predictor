using FootballPredictor.Models.Competitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using FootballPredictor.Models.Fixtures;
using FootballPredictor.Models.People;

namespace FootballPredictor.Models.Predictions
{
    public class OpenPrediction : Prediction, IOpenPrediction
    {
        public OpenPrediction(int id)
            : base(id)
        {

        }


    }
}