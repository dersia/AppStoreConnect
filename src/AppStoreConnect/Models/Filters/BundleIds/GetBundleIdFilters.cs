using AppStoreConnect.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppStoreConnect.Models.Filters.BundleIds
{
    public class GetBundleIdFilters
    {
        public List<string>? FieldsBundleIdsFilter { get; set; }
        public List<string>? FieldsProfilesFilter { get; set; }
        public List<string>? FieldsCapabilitiesFilter { get; set; }
        public List<string>? IncludeFilter { get; set; }
        public int? LimitProfilesFilter { get; set; }


        public static class Keys
        {
            public const string FieldsBundleIds = "&fields[bundleIds]";
            public const string FieldsProfiles = "&fields[profiles]";
            public const string FieldsCapabilities = "&fields[bundleIdCapabilities]";
            public const string Include = "&include";
            public const string LimitProfiles = "&limit[profiles]";
        }

        public static class FieldsBundleIds
        {
            public const string BundleIdCapabilities = "bundleIdCapabilities";
            public const string Identifier = "identifier";
            public const string Name = "name";
            public const string Platform = "platform";
            public const string Profiles = "profiles";
            public const string SeedId = "seedId";
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

        public static class FieldsCapabilities
        {
            public const string BundleId = "bundleId";
            public const string CapabilityType = "capabilityType";
            public const string Settings = "settings";
        }

        public static class Include
        {
            public const string BundleIdCapabilities = "bundleIdCapabilities";
            public const string Profiles = "profiles";
        }
    }
}
