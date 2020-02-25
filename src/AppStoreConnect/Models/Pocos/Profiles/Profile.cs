using AppStoreConnect.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AppStoreConnect.Models.Pocos.Profiles
{
    public class Profile
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("platform")]
        [JsonConverter(typeof(JsonStringEnumMemberConverter))]
        public BundleIdPlatform? Platform { get; set; }
        [JsonPropertyName("profileContent")]
        public string? ProfileContent { get; set; }
        [JsonPropertyName("uuid")]
        public string? Uuid { get; set; }
        [JsonPropertyName("createdDate")]
        public string? CreatedDate { get; set; }
        [JsonPropertyName("profileState")]
        [JsonConverter(typeof(JsonStringEnumMemberConverter))]
        public ProfileState? ProfileState { get; set; }
        [JsonPropertyName("profileType")]
        [JsonConverter(typeof(JsonStringEnumMemberConverter))]
        public ProfileType? ProfileType { get; set; }
        [JsonPropertyName("expirationDate")]
        public string? ExpirationDate { get; set; }
    }
}
