using FootballPredictor.Models.Competitions;
using FootballPredictor.Models.Predictions;

namespace FootballPredictor.Repositories.Predictions
{
    public interface IOpenPredictionRepository
    {
        IOpenPrediction Get(int id);
        void Insert(IOpenPrediction prediction);
        void UpdateScore(int id, IPredictionScore score);
        void Delete(int id);
       
    }
}