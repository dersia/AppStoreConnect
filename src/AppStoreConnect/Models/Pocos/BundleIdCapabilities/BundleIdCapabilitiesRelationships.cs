using AppStoreConnect.Models.Pocos.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AppStoreConnect.Models.Pocos.BundleIdCapabilities
{
    public class BundleIdCapabilitiesRelationships
    {
        [JsonPropertyName("bundleId")]
        public Relationship? BundleId { get; set; }
    }
}
