using FootballPredictor.Models.People;
using FootballPredictor.Models.Security;
using FootballPredictor.Models.Security.WebAPI;
using FootballPredictor.Repositories.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web.Http;

namespace FootballPredictor.Security.Controllers
{
    public class UserLoginsController : ApiController
    {
        private IUserRepository UserRepository { get; set; }


        public UserLoginsController(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        public IHttpActionResult Post(UserLogin userLogin)
        { 
            // Get user from database using username
            var user = UserRepository.GetByUsername(userLogin.username);
            if (user == null || !user.Active)
            {
                return Unauthorized();
            } else
            {
                // Check the entered password against stored user password
                var passwordValid = user.Authenticate(userLogin.password);
                if (passwordValid)
                {
                    // Generate and return the encrypted token
                    var clientAuthorisation = new ClientAuthorisation(user.Id, DateTime.Now);
                    var encryptedUser = RSAClass.Encrypt(Newtonsoft.Json.JsonConvert.SerializeObject(clientAuthorisation));
                    return Ok(encryptedUser);
                } else
                {
                    return Unauthorized();
                }
            }
        }
    }
}
