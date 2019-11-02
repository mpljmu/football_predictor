using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPredictor.Models.Security
{
    public class ClientAuthorisation
    {
        public int UserId { get; private set; }
        public DateTime IssuedAt { get; private set; }


        public ClientAuthorisation(int userId, DateTime issuedAt)
        {
            UserId = userId;
            IssuedAt = issuedAt;
        }
    }
}