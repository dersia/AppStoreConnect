using AppStoreConnect.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AppStoreConnect.Models.Pocos.Users
{
    public class User
    {
        [JsonPropertyName("firstName")]
        public string? FirstName { get; set; }
        [JsonPropertyName("lastName")]
        public string? LastName { get; set; }
        [JsonPropertyName("provisioningAllowed")]
        public bool? ProvisioningAllowed { get; set; }
        [JsonPropertyName("allAppsVisible")]
        public bool? AllAppsVisible { get; set; }
        [JsonPropertyName("roles")]
        public List<UserRoles>? UserRoles { get; set; }
    }
}
