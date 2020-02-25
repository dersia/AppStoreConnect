using AppStoreConnect.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AppStoreConnect.Models.Pocos.Apps
{
    public class AppInformation
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        [JsonPropertyName("type")]
        [JsonConverter(typeof(JsonStringEnumMemberConverter))]
        public ResourceTypes? Type { get; set; }
        [JsonPropertyName("relationships")]
        public AppRelationships? Relationships { get; set; }
        [JsonPropertyName("attributes")]
        public App? App { get; set; }
    }
}
