using FootballPredictor.Models.Competitions;
using FootballPredictor.Models.Connections;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;

namespace FootballPredictor.Controllers.Competitions
{
    public class PredictionsController : ApiController
    {
        private IKernel Kernel = new StandardKernel();
        private IDatabaseConnection DatabaseConnection
        {
            get
            {
                Kernel.Load(Assembly.GetExecutingAssembly());
                return Kernel.Get<IDatabaseConnection>();
            }
        }

        // GET: api/Predictions
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Predictions/5
        public Prediction Get(int id)
        {
            var prediction = new Prediction(id, DatabaseConnection);
            prediction.PopulateObject();
            return prediction;
        }

        // POST: api/Predictions
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Predictions/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Predictions/5
        public void Delete(int id)
        {
        }
    }
}
