using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AppStoreConnect.Models.Filters.Profiles
{
    public class GetLinkedProfileFilters
    {
        public List<string>? FieldsProfilesFilter { get; set; }
        public int? LimitProfilesFilter { get; set; }

        public static class Keys
        {
            public const string FieldsProfiles = "&fields[profiles]";
            public const string Limit = "&limit";
        }

        public static class FieldsProfiles
        {
            public const string BundleId = "bundleId";
            public const string Certificates = "certificates";
            public const string CreatedDate = "createdDate";
            public const string Devices = "devices";
            public const string ExpirationDate = "expirationDate";
            public const string Name = "name";
            public const string Platform = "platform";
            public const string ProfileContent = "profileContent";
            public const string ProfileState = "profileState";
            public const string ProfileType = "profileType";
            public const string Uuid = "uuid";
        }
    }
}
