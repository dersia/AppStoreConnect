using AppStoreConnect.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AppStoreConnect.Models.Pocos.Capabilities
{
    public class CapabilitySetting
    {
        [JsonPropertyName("allowedInstances")]
        [JsonConverter(typeof(JsonStringEnumMemberConverter))]
        public AllowedInstances? AllowedInstances { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("enabledByDefault")]
        public bool? EnabledByDefault { get; set; }
        [JsonPropertyName("key")]
        [JsonConverter(typeof(JsonStringEnumMemberConverter))]
        public CapabilitySettingKeys? Key { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("visible")]
        public bool? Visible { get; set; }
        [JsonPropertyName("minInstances")]
        public int? MinInstances { get; set; }
        [JsonPropertyName("options")]
        public List<CapabilityOption>? Options { get; set; }
    }
}
