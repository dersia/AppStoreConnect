using System;
using System.Collections.Generic;
using System.Text;

namespace AppStoreConnect.Models.Filters.BundleIds
{
    public class GetLinkedBundleIdFilter
    {
        public List<string>? FieldsBundleIdsFilter { get; set; }


        public static class Keys
        {
            public const string FieldsBundleIds = "&fields[bundleIds]";
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
    }
}
