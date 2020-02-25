using AppStoreConnect.Abstractions;
using AppStoreConnect.Endpoints.BundleIdCapabilities;
using AppStoreConnect.Endpoints.BundleIds;
using AppStoreConnect.Endpoints.Certificates;
using AppStoreConnect.Endpoints.Devices;
using AppStoreConnect.Endpoints.Profiles;
using AppStoreConnect.Endpoints.UserInvitations;
using AppStoreConnect.Endpoints.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppStoreConnect
{
    public class AppStoreConnectionClient
    {
        private readonly string _token;

        private AppStoreConnectionClient(string token) 
            => _token = token;

        public static AppStoreConnectionClient CreateClient(string token) 
            => new AppStoreConnectionClient(token);

        public IBundleIdCapabilities BundleIdCapabilities => new BundleIdCapabilities(_token);
        public IBundleIds BundleIds => new BundleIds(_token);
        public ICertificates Certificates => new Certificates(_token);
        public IDevices Devices => new Devices(_token);
        public IProfiles Profiles => new Profiles(_token);
        public IUserInvitations UserInvitations => new UserInvitations(_token);
        public IUsers Users => new Users(_token);
    }
}
