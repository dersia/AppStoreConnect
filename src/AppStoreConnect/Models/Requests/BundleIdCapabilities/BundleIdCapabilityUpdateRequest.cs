using AppStoreConnect.Models.Pocos.BundleIdCapabilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AppStoreConnect.Models.Requests.BundleIdCapabilities
{
    public class BundleIdCapabilityUpdateRequest
    {
        [JsonPropertyName("data")]
        public BundleIdCapabilityInformation? BundleIdCapabilityInformation { get; set; }
    }
}
