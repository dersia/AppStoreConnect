using System;
using System.Collections.Generic;
using System.Text;

namespace AppStoreConnect.Constants
{
    public static class EndpointConstants
    {
        public const string AppStoreConnectBaseUrl = "https://api.appstoreconnect.apple.com/v1";
        public const string JsonContentType = "application/json";

        public static class Enpoints
        {
            public const string Users = "users";
            public const string Relationships = "relationships";
            public const string VisibleApps = "visibleApps";
            public const string UserInvitations = "userInvitations";
            public const string BundleIds = "bundleIds";
            public const string BundleId = "bundleId";
            public const string BundleIdCapabilities = "bundleIdCapabilities";
            public const string Profiles = "profiles";
            public const string Certificates = "certificates";
            public const string Devices = "devices";
        }
    }
}
