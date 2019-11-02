using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;

namespace FootballPredictor.Models.People
{
    public class Password : IPassword
    {
        private static int IterationCount = 1000;
        public string TextPassword { get; private set; }
        private byte[] Hash { get; set; }
        private byte[] Salt { get; set; }
        

        public Password(string textPassword)
        {
            TextPassword = textPassword;
        }
        public Password(string textPassword, byte[] salt)
        {
            TextPassword = textPassword;
            Salt = salt;
        }
        public Password(byte[] hash, byte[] salt)
        {
            Hash = hash;
            Salt = salt;
        }


        public static byte[] GenerateHash(byte[] salt, string textPassword)
        {
            // Run hashing algorithm and return hashed password as byte array
            var rfc = new Rfc2898DeriveBytes(textPassword, salt, IterationCount);
            var hash = rfc.GetBytes(16);
            return hash;
        }
        public bool CheckPassword(string textPassword)
        {
            var hashToCheck = Password.GenerateHash(Salt, textPassword);
            if (Hash.SequenceEqual(hashToCheck))
            {
                return true;
            } else
            {
                return false;
            }
        }

    }
}