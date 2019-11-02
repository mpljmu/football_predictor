using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPredictor.Models.Security
{
    using System;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    namespace WebAPI
    {
        public class RSAClass
        {
            // 2048 bit key, allows encyption of 256 bytes of data
            private static string _privateKey = "<RSAKeyValue><Modulus>u5uwhObZ9vvRA/YtONvsz312rRg6I+9k5UUX6dzRugdQF42L8MuDcLsQlSEBektiTsQjpbm9XZn13foTy5JL9X5X7QD4NV6l/jjFmA6/FGIhmqgMcr6rk0/NrPthMj6MprN7jbudzti6bZW7E4jPZZMgzh9cSQ442wcUYcp3cmJDAThFxccB7pTlgmXp/gsRW96nHbGFAa6Gjxfz6iUshAkO9wkXpg2wpTaSnxhOORkMC/gBCKpnFRIBRhACoqGSZhadzAZq8rzGl+EhKvSaVHNl9RqBvWCHeAhS9HQXwWTUTyD3ttfSv+X1w48jopRa95cwA4hCONn8YRHOZ6sf3Q==</Modulus><Exponent>AQAB</Exponent><P>xt0mG2yj61qiLNQXsr96mokL4FdFwo+H2rs7e2uyT1oH9Kqc9HATFDaQH6vbf8TzpGHnXUjA9er1wzT2xge6wHMsIawvkorKpsOF01rpIXdiGDZZKUPyarh2hhN0iGSF1krwyXkZW68qfm0u9qPOtGk7YoNCkdkzOS5ZjaF791s=</P><Q>8YKpFEM4BmUoopKbI4yN3/wPu9gVt0Pb6jdZvAP2n/HTYuaFtxU3CxuJT+79AfP0C1SMWWSudzHY91RcAPvWqcpcsYkaQzqw2m7fa2xtRMhd3J4zxt5EHC4FZmm6+YRLfLL2fRDHRRhCnhfMhck5nUZ8/kqAUNTbPT9HCBmVIyc=</Q><DP>Et1dxRI8RpJVeh0wllNVxR0lFEYTJw7Im3ZRgTbJNn/a61nYA9Qx6yP17hs2eltrpXdoJFBHhcyhPcBjfIu1KpaCZDtaU/N4n/NCWbdxECysEJHvSVvZvkf7bmKgFmQ60gZP6zziq/Dk/hNLdjg53qFw8bpz8TQCiPUdp7Le+Ks=</DP><DQ>Mc7MqA0k5MzAEKdDr5UxPVxysj7iW6V3GVrI+ummV148Rk1cjmGltHi9XOrg6yIw1pVdTKJjCNoS8Q9I2jsWDnZZn5OzAuJ7ztDG6xS1hFX+ZZ2K+Bym11j2bCSqFwOdvd36z9hCAJH8SzaFS6Iwa6s55AfhZso/XOZL8/Oyukk=</DQ><InverseQ>MjUrhJe+0fVoxxj/cw44BeystyRblsfXa6q41CD+MHfa3nffR2e2aBPvhDr2lj7OSv/nNami59DVIv9JtsqAapp1Fze/ype3GNtl8iV5/FfYJoTI+phZ2EeihJab/wpo3dfaZQ0QVovzKl3WdsuMVFBa1QJ5dVjHU/G5NCRtAWA=</InverseQ><D>nXEX30C41MwZacCzzM7L2olJChSV3khuHPYyDmHxY7P1Y/623Rp9sSJb1TsAuXgABWgXHmJU5/Nn4aSX7jRVKK2h7lTs+CT5GvLb6DMf6mQ8HUVARR98b8D+M1g3Bmp1sQRZAOXdlpRNR9/rQoaCvpSNaE3rLagQ0McNNNAsE8GvNawyC6zcRNf1W8OaGL6RhaPjTYbKFUxJVSZmNEiFO9sp2QtZ3Nki4WRHWa6M2VwTR/UgTZWd59nVyFjqax+tA/OSe5PBgVwQeA1SPRWsUV8R5ibIdjX2ii8QYrEdK8cGNHs9FCZTOll4v0o8ffSKaVU0AcUk3B7WyClRTZBoRQ==</D></RSAKeyValue>";
            private static string _publicKey = "<RSAKeyValue><Modulus>u5uwhObZ9vvRA/YtONvsz312rRg6I+9k5UUX6dzRugdQF42L8MuDcLsQlSEBektiTsQjpbm9XZn13foTy5JL9X5X7QD4NV6l/jjFmA6/FGIhmqgMcr6rk0/NrPthMj6MprN7jbudzti6bZW7E4jPZZMgzh9cSQ442wcUYcp3cmJDAThFxccB7pTlgmXp/gsRW96nHbGFAa6Gjxfz6iUshAkO9wkXpg2wpTaSnxhOORkMC/gBCKpnFRIBRhACoqGSZhadzAZq8rzGl+EhKvSaVHNl9RqBvWCHeAhS9HQXwWTUTyD3ttfSv+X1w48jopRa95cwA4hCONn8YRHOZ6sf3Q==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
            private static UnicodeEncoding _encoder = new UnicodeEncoding();


            public static ClientAuthorisation Decrypt(byte[] data)
            {
                var rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(_privateKey);
                // Decrypt the byte array in to the serialized json object - still a byte array
                var decryptedByte = rsa.Decrypt(data, false);
                // Get the string from the byte array 
                // and the parse that in to a ClientAuthorisation object
                return Newtonsoft.Json.JsonConvert.DeserializeObject<ClientAuthorisation>(_encoder.GetString(decryptedByte));
            }

            public static string Encrypt(string data)
            {
                var dataToEncrypt = _encoder.GetBytes(data);
                var rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(_publicKey);
                var encryptedByteArray = rsa.Encrypt(dataToEncrypt, false).ToArray();
                // Convert the byte array to hexadecimal
                var encryptedHex = "";
                foreach(var encryptedByte in encryptedByteArray)
                {
                    encryptedHex += encryptedByte.ToString("x2");
                }
                return encryptedHex;
            }
        }
    }
}