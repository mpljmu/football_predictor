using FootballPredictor.Models.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using FootballPredictor.Models.People;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using FootballPredictor.Models.Security.WebAPI;

namespace FootballPredictor.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private IDatabaseConnection DatabaseConnection { get; set; }


        public UserRepository(IDatabaseConnection databaseConnection)
        {
            DatabaseConnection = databaseConnection;
        }


        public IUser Get(int id)
        {
            try
            {
                using (var connection = DatabaseConnection.NewConnection())
                {
                    var user = connection.Query<User, Password, User>(
                        @"SELECT
	                        Id,
	                        Username,
	                        Active,
	                        Forename,
	                        Surname,
	                        EmailAddress,
                            [Hash],
                            Salt
                          FROM
	                        [User]
                          WHERE
	                        Id = @Id",
                        (thisUser, password) =>
                        {
                            thisUser = new User(thisUser.Id, thisUser.Username, thisUser.Active, thisUser.Forename, thisUser.Surname, thisUser.EmailAddress, password);
                            return thisUser;
                        },
                        new
                        {
                            Id = id
                        },
                        splitOn: "Id,Hash"
                    ).Single();
                    return user;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IUser GetByUsername(string username)
        {
            try
            {
                using (var connection = DatabaseConnection.NewConnection())
                {
                    var id = connection.Query<int>(
                        @"SELECT
	                        Id
                          FROM
	                        [User]
                          WHERE
	                        Username = @Username",
                        new
                        {
                            Username = username
                        }
                    ).FirstOrDefault();
                    var user = Get(id);
                    return user;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token">Hexadecimal string from client</param>
        /// <returns></returns>
        public IUser GetByAuthorisationToken(string token)
        {
            try
            {
                var tokenByteArray = SoapHexBinary.Parse(token).Value;
                var decryptedClientUser = RSAClass.Decrypt(tokenByteArray);
                var user = Get(decryptedClientUser.UserId);
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}