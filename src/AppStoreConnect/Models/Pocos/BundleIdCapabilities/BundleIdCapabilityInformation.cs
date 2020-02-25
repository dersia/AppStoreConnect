using AppStoreConnect.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AppStoreConnect.Models.Pocos.BundleIdCapabilities
{
    public class BundleIdCapabilityInformation
    {
        [JsonPropertyName("attributes")]
        public BundleIdCapability? BundleIdCapability { get; set; }
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        [JsonPropertyName("links")]
        public Dictionary<string, string>? Links { get; set; }
        [JsonPropertyName("relationships")]
        public BundleIdCapabilitiesRelationships? Relationships { get; set; }
        [JsonPropertyName("type")]
        [JsonConverter(typeof(JsonStringEnumMemberConverter))]
        public ResourceTypes? Type { get; set; }
    }
}
