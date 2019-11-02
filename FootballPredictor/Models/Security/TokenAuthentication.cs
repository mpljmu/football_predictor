using FootballPredictor.Models.Security.WebAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;

namespace FootballPredictor.Models.Security
{
    public class TokenAuthenticationAttribute : Attribute, IAuthenticationFilter
    {
        public bool AllowMultiple
        {
            get { return true; }
        }

        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            try
            {
                var token = context.ActionContext.Request.Headers.GetValues("Authorisation-Token").First();
                var tokenByteArray = SoapHexBinary.Parse(token).Value;
                var decryptedClientUser = RSAClass.Decrypt(tokenByteArray);
                return Task.FromResult(0);
            }
            catch (Exception ex)
            {
                // Log to db
                context.ErrorResult = new AuthenticationFailureResponse("Authentication failure", context.Request);
                return Task.FromResult(0);
            }
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }
    }
}