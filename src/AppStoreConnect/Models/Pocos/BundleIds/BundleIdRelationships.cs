using AppStoreConnect.Models.Pocos.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AppStoreConnect.Models.Pocos.BundleIds
{
    public class BundleIdRelationships
    {
        [JsonPropertyName("profiles")]
        public Relationships? ProfileIds { get; set; }
        [JsonPropertyName("bundleIdCapabilities")]
        public Relationships? CapabilityIds { get; set; }
    }
}
