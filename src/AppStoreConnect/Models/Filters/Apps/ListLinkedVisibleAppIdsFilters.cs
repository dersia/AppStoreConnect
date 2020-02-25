using System;
using System.Collections.Generic;
using System.Text;

namespace AppStoreConnect.Models.Filters.Apps
{
    public class ListLinkedVisibleAppIdsFilters
    {
        public int? LimitVisibleAppsFilter { get; set; }

        public static class Keys
        {
            public const string LimitVisibleApps = "&limit";
        }
    }
}
