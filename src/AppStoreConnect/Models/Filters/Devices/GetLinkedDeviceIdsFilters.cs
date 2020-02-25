using System;
using System.Collections.Generic;
using System.Text;

namespace AppStoreConnect.Models.Filters.Devices
{
    public class GetLinkedDeviceIdsFilters
    {
        public int? LimitFilter { get; set; }

        public static class Keys
        {
            public const string Limit = "&limit";
        }
    }
}
