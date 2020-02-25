using System;
using System.Collections.Generic;
using System.Text;

namespace AppStoreConnect.Models.Filters.Certificates
{
    public class GetLinkedCertificateIdsFilters
    {
        public int? LimitFilter { get; set; }

        public static class Keys
        {
            public const string Limit = "&limit";
        }
    }
}
