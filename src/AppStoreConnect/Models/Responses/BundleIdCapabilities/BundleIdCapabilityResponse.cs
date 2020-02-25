using AppStoreConnect.Models.Pocos.BundleIdCapabilities;
using AppStoreConnect.Models.Responses.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AppStoreConnect.Models.Responses.BundleIdCapabilities
{
    public class BundleIdCapabilityResponse : ApplicationResponse
    {
        [JsonPropertyName("data")]
        public BundleIdCapabilityInformation? BundleIdCapability { get; set; }
        [JsonPropertyName("links")]
        public Dictionary<string, string>? Links { get; set; }
    }
}
