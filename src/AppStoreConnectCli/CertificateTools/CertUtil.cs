using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Prng;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace AppStoreConnectCli.CertificateTools
{
    public class CertUtil
    {
        public (Pkcs10CertificationRequest csr, AsymmetricCipherKeyPair? keyPair) CreateCertificateSigningRequestInteractive(int keyLength)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Creating a Certificate Signing Request");
            var countryCode = GetCountryCode();
            var stateOrProvince = GetStateOrProvince();
            var localityOrCity = GetLocalityOrCity();
            var company = GetCompany();
            var unit = GetOrganizationalUnit();
            var commonName = GetCommonName();
            var email = GetEmailAddress();
            var (csr, keyPair) = CreateCertificateSigningRequest(commonName, countryCode, email, stateOrProvince, localityOrCity, company, unit, keyLength);
            Console.ResetColor();
            return (csr, keyPair);

            static string GetCountryCode()
            {
                Console.Write("Country Code* [C]: ");
                var countryCode = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(countryCode))
                {
                    Console.WriteLine("Country Code is required");
                    return GetCountryCode();
                }
                return countryCode;
            }

            static string GetStateOrProvince()
            {
                Console.Write("State or Province [ST]: ");
                var stateOrProvince = Console.ReadLine();
                return stateOrProvince;
            }

            static string GetLocalityOrCity()
            {
                Console.Write("Locality or City [L]: ");
                var localityOrCity = Console.ReadLine();
                return localityOrCity;
            }

            static string GetCompany()
            {
                Console.Write("Company [O]: ");
                var company = Console.ReadLine();
                return company;
            }

            static string GetOrganizationalUnit()
            {
                Console.Write("Organizational Unit [OU]: ");
                var unit = Console.ReadLine();
                return unit;
            }

            static string GetCommonName()
            {
                Console.Write("Common Name* [CN]: ");
                var commonName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(commonName))
                {
                    Console.WriteLine("Common Name is required");
                    return GetCommonName();
                }
                return commonName;
            }

            static string GetEmailAddress()
            {
                Console.Write("Email Address [emailAddress]: ");
                var email = Console.ReadLine();
                return email;
            }
        }

        public (Pkcs10CertificationRequest csr, AsymmetricCipherKeyPair? keyPair) CreateCertificateSigningRequest(string commonName, string countryCode, string? emailAddress, string? stateOrProvince = null, string? localityOrCity = null, string? companyName = null, string? unit = null, int keyLength = 2048)
        {
            if(keyLength < 1)
            {
                keyLength = 2048;
            }
            using var key = RSA.Create(keyLength);
            var builder = new StringBuilder($"CN={commonName}, C={countryCode}");
            if(!string.IsNullOrWhiteSpace(stateOrProvince))
            {
                builder.Append($", ST={stateOrProvince}");
            }
            if (!string.IsNullOrWhiteSpace(localityOrCity))
            {
                builder.Append($", L={localityOrCity}");
            }
            if (!string.IsNullOrWhiteSpace(companyName))
            {
                builder.Append($", O={companyName}");
            }
            if (!string.IsNullOrWhiteSpace(unit))
            {
                builder.Append($", OU={unit}");
            }
            if (!string.IsNullOrWhiteSpace(emailAddress))
            {
                builder.Append($", emailAddress={emailAddress}");
            }
            var issuer = new X509Name(builder.ToString());
            var subject = new X509Name(builder.ToString());
            var randomGenerator = new CryptoApiRandomGenerator();
            var random = new SecureRandom(randomGenerator);
            var keyGenerationParameters = new KeyGenerationParameters(random, keyLength);

            var keyPairGenerator = new RsaKeyPairGenerator();
            keyPairGenerator.Init(keyGenerationParameters);
            var issuerKeyPair = keyPairGenerator.GenerateKeyPair();

            var csr = new Pkcs10CertificationRequest("SHA256WITHRSA", subject, issuerKeyPair.Public, null, issuerKeyPair.Private);
            return (csr, issuerKeyPair);
        }

        public byte[] CreateP12(byte[] cer, string? privateKey = null, string? password = null)
        {
            var cert = new X509Certificate2(cer);
            if (privateKey is not null) 
            {
                using var rsa = RSA.Create();
                rsa.ImportFromPem(privateKey);
                cert = cert.CopyWithPrivateKey(rsa);
            }
            return cert.Export(X509ContentType.Pkcs12, password);
        }
    }
}
