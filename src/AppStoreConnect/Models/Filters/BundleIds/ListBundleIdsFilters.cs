using AppStoreConnect.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppStoreConnect.Models.Filters.BundleIds
{
    public class ListBundleIdsFilters
    {
        public List<string>? FieldsBundleIdsFilter { get; set; }
        public List<string>? FieldsProfilesFilter { get; set; }
        public List<string>? FieldsCapabilitiesFilter { get; set; }
        public List<string>? IncludeFilter { get; set; }
        public int? LimitBundleIdsFilter { get; set; }
        public List<string>? SortBy { get; set; }
        public List<string>? IdFilter { get; set; }
        public List<string>? IdentifierFilter { get; set; }
        public List<string>? NameFilter { get; set; }
        public List<BundleIdPlatform>? PlatformFilter { get; set; }
        public List<string>? SeedIdFilter { get; set; }
        public int? LimitProfilesFilter { get; set; }


        public static class Keys
        {
            public const string FieldsBundleIds = "&fields[bundleIds]";
            public const string FieldsProfiles = "&fields[profiles]";
            public const string FieldsCapabilities = "&fields[bundleIdCapabilities]";
            public const string Include = "&include";
            public const string Limit = "&limit";
            public const string Sort = "&sort";
            public const string FilterId = "&filter[id]";
            public const string FilterIdentifier = "&filter[identifier]";
            public const string FilterName = "&filter[name]";
            public const string FilterPlatform = "&filter[platform]";
            public const string FilterSeedId = "&filter[seedId]";
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

        public static class Sort
        {
            public const string Id = "id";
            public const string IdDesc = "-id";
            public const string Name = "name";
            public const string NameDesc = "-name";
            public const string Platform = "platform";
            public const string PlatformDesc = "-platform";
            public const string SeedId = "seedId";
            public const string SeedIdDesc = "-seedId";
        }
    }
}
