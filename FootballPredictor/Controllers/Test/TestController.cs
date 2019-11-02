using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FootballPredictor.Models.Connections;
using FootballPredictor.Models.Security;
using FootballPredictor.Repositories.Users;

namespace FootballPredictor.Controllers.Test
{
    [TokenAuthentication]
    public class TestController : ApiController
    {
        private IUserRepository UserRepository{ get; set; }

        public TestController(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        public IHttpActionResult Get()
        {
            var token = Request.Headers.GetValues("Authorisation-Token").First();
            var user = UserRepository.GetByAuthorisationToken(token);
            return Ok(user);
        }
    }
}
