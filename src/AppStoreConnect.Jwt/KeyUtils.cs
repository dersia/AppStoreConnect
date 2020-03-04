using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Security.Cryptography;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

namespace AppStoreConnect.Jwt
{
    public static class KeyUtils
    {
        public static CngKey GetPrivateKey(string p8FilePath)
        {
            using var reader = File.OpenText(p8FilePath);
            var ecPrivateKeyParameters = (ECPrivateKeyParameters)new PemReader(reader).ReadObject();
            var x = ecPrivateKeyParameters.Parameters.G.AffineXCoord.GetEncoded();
            var y = ecPrivateKeyParameters.Parameters.G.AffineYCoord.GetEncoded();
            var d = ecPrivateKeyParameters.D.ToByteArrayUnsigned();
            return EccKey.New(x, y, d);
        }

        public static CngKey GetPrivateKeyFromString(string p8FileContent)
        {
            using var reader = new StringReader(p8FileContent);
            var ecPrivateKeyParameters = (ECPrivateKeyParameters)new PemReader(reader).ReadObject();
            var x = ecPrivateKeyParameters.Parameters.G.AffineXCoord.GetEncoded();
            var y = ecPrivateKeyParameters.Parameters.G.AffineYCoord.GetEncoded();
            var d = ecPrivateKeyParameters.D.ToByteArrayUnsigned();
            return EccKey.New(x, y, d);
        }

        public static string CreateTokenAndSign(string privateKeyFilePath, string kid, string issuer, string audience, TimeSpan timeout = default)
        {
            if(timeout == default)
            {
                timeout = TimeSpan.FromMinutes(20);
            } 
            else if(timeout.TotalMinutes > 20)
            {
                throw new ArgumentOutOfRangeException(nameof(timeout));
            }
            var privateKey = GetPrivateKey(privateKeyFilePath);
            var epochNow = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            var expires = (int)DateTime.UtcNow.Add(timeout).Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            var payload = new Dictionary<string, object>
            {
                {"iss", issuer },
                {"aud", audience },
                {"exp", expires }
            };
            var extraHeaders = new Dictionary<string, object>
            {
                {"kid", kid },
                {"typ", "JWT" }
            };
            return Jose.JWT.Encode(payload, privateKey, Jose.JwsAlgorithm.ES256, extraHeaders);
        }

        public static string CreateTokenAndSignFromString(string privateKeyFileContent, string kid, string issuer, string audience, TimeSpan timeout = default)
        {
            if (timeout == default)
            {
                timeout = TimeSpan.FromMinutes(20);
            }
            else if (timeout.TotalMinutes > 20)
            {
                throw new ArgumentOutOfRangeException(nameof(timeout));
            }
            var privateKey = GetPrivateKeyFromString(privateKeyFileContent);
            var epochNow = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            var expires = (int)DateTime.UtcNow.Add(timeout).Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            var payload = new Dictionary<string, object>
            {
                {"iss", issuer },
                {"aud", audience },
                {"exp", expires }
            };
            var extraHeaders = new Dictionary<string, object>
            {
                {"kid", kid },
                {"typ", "JWT" }
            };
            return Jose.JWT.Encode(payload, privateKey, Jose.JwsAlgorithm.ES256, extraHeaders);
        }
    }
}
