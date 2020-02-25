using AppStoreConnect.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AppStoreConnect.Models.Pocos.Capabilities
{
    public class CapabilityOption
    {
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("enabled")]
        public bool? Enabled { get; set; }
        [JsonPropertyName("enabledByDefault")]
        public bool? EnabledByDefault { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("supportsWildcard")]
        public bool? SupportsWildcard { get; set; }
        [JsonPropertyName("key")]
        [JsonConverter(typeof(JsonStringEnumMemberConverter))]
        public CapabilityOptionKeys? Key { get; set; }
    }
}
