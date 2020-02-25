using AppStoreConnect.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AppStoreConnect.Models.Pocos.BundleIds
{
    public class BundleId
    {
        [JsonPropertyName("identifier")]
        public string? Identifier { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("platform")]
        [JsonConverter(typeof(JsonStringEnumMemberConverter))]
        public BundleIdPlatform? Platform { get; set; }
        [JsonPropertyName("seedId")]
        public string? SeedId { get; set; }
    }
}
