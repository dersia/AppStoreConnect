using AppStoreConnect.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppStoreConnect.Models.Filters.BundleIdCapabilities
{
    public class GetLinkedBundleIdCapabilityFilters
    {
        public List<string>? FieldsBundleIdCapabilitiesFilter { get; set; }
        public int? LimitFilter { get; set; }


        public static class Keys
        {
            public const string FieldsBundleIdCapabilities = "&fields[bundleIdCapabilities]";
            public const string Limit = "&limit";
        }

        public static class FieldsBundleIdCapabilities
        {
            public const string BundleId = "bundleId";
            public const string CapabilityType = "capabilityType";
            public const string Settings = "settings";
        }
    }
}
