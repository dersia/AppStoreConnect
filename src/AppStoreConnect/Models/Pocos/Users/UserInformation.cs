using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using AppStoreConnect.Models.Pocos.Common;
using AppStoreConnect.Models.Pocos.Users;
using AppStoreConnect.Models.Enums;

namespace AppStoreConnect.Models.Pocos.Users
{
    public class UserInformation
    {
        [JsonPropertyName("type")]
        [JsonConverter(typeof(JsonStringEnumMemberConverter))]
        public ResourceTypes? Type { get; set; }
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        [JsonPropertyName("attributes")]
        public User? User { get; set; }
        [JsonPropertyName("relationships")]
        public UserRelationships? Relationships { get; set; }
        [JsonPropertyName("links")]
        public Dictionary<string, string>? Links { get; set; }
    }
}
