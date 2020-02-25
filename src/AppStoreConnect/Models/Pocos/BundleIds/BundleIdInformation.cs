using AppStoreConnect.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AppStoreConnect.Models.Pocos.BundleIds
{
    public class BundleIdInformation
    {
        [JsonPropertyName("attributes")]
        public BundleId? BundleId { get; set; }
        [JsonPropertyName("type")]
        [JsonConverter(typeof(JsonStringEnumMemberConverter))]
        public ResourceTypes? Type { get; set; }
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        [JsonPropertyName("relationships")]
        public BundleIdRelationships? Relationsips { get; set; }
    }
}
