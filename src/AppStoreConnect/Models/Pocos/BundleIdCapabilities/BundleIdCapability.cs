using AppStoreConnect.Models.Enums;
using AppStoreConnect.Models.Pocos.Capabilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AppStoreConnect.Models.Pocos.BundleIdCapabilities
{
    public class BundleIdCapability
    {
        [JsonPropertyName("capabilityType")]
        [JsonConverter(typeof(JsonStringEnumMemberConverter))]
        public CapabilityType? CapabilityType { get; set; }
        [JsonPropertyName("settings")]
        public List<CapabilitySetting>? CapabilitySettings { get; set; }
    }
}
