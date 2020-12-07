using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using Microsoft.IdentityModel.JsonWebTokens;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto;

namespace AppStoreConnect.Jwt
{
    public static class KeyUtils
    {
        private static void GetPrivateKey(TextReader reader, ECDsa ecDSA)
        {
            var ecPrivateKeyParameters = (ECPrivateKeyParameters)new PemReader(reader).ReadObject();
            var q = ecPrivateKeyParameters.Parameters.G.Multiply(ecPrivateKeyParameters.D);
            var pub = new ECPublicKeyParameters(ecPrivateKeyParameters.AlgorithmName, q, ecPrivateKeyParameters.PublicKeyParamSet);
            var x = pub.Q.AffineXCoord.GetEncoded();
            var y = pub.Q.AffineYCoord.GetEncoded();
            var d = ecPrivateKeyParameters.D.ToByteArrayUnsigned();
            var msEcp = new ECParameters { Curve = ECCurve.NamedCurves.nistP256, Q = { X = x, Y = y }, D = d };
            msEcp.Validate();
            ecDSA.ImportParameters(msEcp);
        }

        private static TextReader GetReaderFromString(string p8FileContent) => new StringReader(p8FileContent);
        private static TextReader GetReaderFromFile(string p8FileContent) => File.OpenText(p8FileContent);

        public static string CreateTokenAndSign(string privateKeyFilePath, string kid, string issuer, string audience, TimeSpan timeout = default)
        {
            using var reader = GetReaderFromFile(privateKeyFilePath);
            return CreateTokenAndSignInternal(reader, kid, issuer, audience, timeout);
        }

        public static string CreateTokenAndSignFromString(string privateKeyFileContent, string kid, string issuer, string audience, TimeSpan timeout = default)
        {
            using var reader = GetReaderFromString(privateKeyFileContent);
            return CreateTokenAndSignInternal(reader, kid, issuer, audience, timeout);
        }

        private static string CreateTokenAndSignInternal(TextReader reader, string kid, string issuer, string audience, TimeSpan timeout = default)
        {
            if (timeout == default)
            {
                timeout = TimeSpan.FromMinutes(20);
            }
            else if (timeout.TotalMinutes > 20)
            {
                throw new ArgumentOutOfRangeException(nameof(timeout));
            }
            using var ecDSA = ECDsa.Create();
            GetPrivateKey(reader, ecDSA);

            var securityKey = new ECDsaSecurityKey(ecDSA) { KeyId = kid };
            var credentials = new SigningCredentials(securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.EcdsaSha256);

            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = issuer,
                Audience = audience,
                Expires = DateTime.UtcNow.Add(timeout),
                TokenType = "JWT",
                SigningCredentials = credentials
            };

            var handler = new Microsoft.IdentityModel.JsonWebTokens.JsonWebTokenHandler();
            var encodedToken = handler.CreateToken(descriptor);
            return encodedToken;
        }
    }
}
