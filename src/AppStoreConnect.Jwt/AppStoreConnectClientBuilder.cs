using System;

namespace AppStoreConnect.Jwt
{
    public class AppStoreConnectClientBuilder
    {
        private string? _pathToP8;
        private string? _kid;
        private string? _issuer;
        private string? _audience;

        public string? _token;
        public string? _config;

        private AppStoreConnectClientBuilder() { }

        public static AppStoreConnectClientBuilder GetBuilder() 
            => new AppStoreConnectClientBuilder();

        public AppStoreConnectClientBuilder FromCer(string pathToP8)
        {
            if (string.IsNullOrWhiteSpace(pathToP8))
            {
                throw new ArgumentNullException(nameof(pathToP8));
            }
            _pathToP8 = pathToP8;
            return this;
        }

        public AppStoreConnectClientBuilder WithKid(string kid)
        {
            if (string.IsNullOrWhiteSpace(kid))
            {
                throw new ArgumentNullException(nameof(kid));
            }
            _kid = kid;
            return this;
        }

        public AppStoreConnectClientBuilder WithIssuer(string issuer)
        {
            if (string.IsNullOrWhiteSpace(issuer))
            {
                throw new ArgumentNullException(nameof(issuer));
            }
            _issuer = issuer;
            return this;
        }

        public AppStoreConnectClientBuilder WithAudience(string audience)
        {
            if (string.IsNullOrWhiteSpace(audience))
            {
                throw new ArgumentNullException(nameof(audience));
            }
            _audience = audience;
            return this;
        }

        public AppStoreConnectClientBuilder FromToken(string token)
        {
            _token = token;
            return this;
        }

        public AppStoreConnectClientBuilder FromConfig(string config)
        {
            _config = config;
            return this;
        }

        public AppStoreConnectionClient Build()
        {
            if(string.IsNullOrWhiteSpace(_token) && (string.IsNullOrWhiteSpace(_pathToP8) || string.IsNullOrWhiteSpace(_kid) || string.IsNullOrWhiteSpace(_issuer) || string.IsNullOrWhiteSpace(_audience)))
            {
                throw new ArgumentException("Not all needed information provided");
            }
            if(!string.IsNullOrWhiteSpace(_token))
            {
                return AppStoreConnectionClient.CreateClient(_token);
            }
#pragma warning disable CS8604 // Possible null reference argument.
            var token = KeyUtils.CreateTokenAndSign(_pathToP8, _kid, _issuer, _audience);
#pragma warning restore CS8604 // Possible null reference argument.
            return AppStoreConnectionClient.CreateClient(token);
        }
    }
}
