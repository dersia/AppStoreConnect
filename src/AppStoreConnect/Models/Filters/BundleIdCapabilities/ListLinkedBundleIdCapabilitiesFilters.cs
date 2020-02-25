using AppStoreConnect.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppStoreConnect.Models.Filters.BundleIdCapabilities
{
    public class ListLinkedBundleIdCapabilitiesFilters
    {
        public int? LimitBundleIdCapabilitiesFilter { get; set; }


        public static class Keys
        {
            public const string Limit = "&limit";
        }
    }
}
