using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AppStoreConnect.Models.Filters.Profiles
{
    public class ListLinkedProfilesFilters
    {
        public int? LimitProfilesFilter { get; set; }

        public static class Keys
        {
            public const string Limit = "&limit";
        }
    }
}
