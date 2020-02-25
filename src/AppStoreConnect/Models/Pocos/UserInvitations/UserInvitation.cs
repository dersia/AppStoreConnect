using AppStoreConnect.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AppStoreConnect.Models.Pocos.UserInvitations
{
    public class UserInvitation
    {
        [JsonPropertyName("email")]
        public string? Email { get; set; }
        [JsonPropertyName("firstName")]
        public string? FirstName { get; set; }
        [JsonPropertyName("lastName")]
        public string? LastName { get; set; }
        [JsonPropertyName("roles")]
        public List<UserRoles>? Roles { get; set; }
        [JsonPropertyName("expirationDate")]
        public string? ExpirationDate { get; set; }
        [JsonPropertyName("provisioningAllowed")]
        public bool? ProvisioningAllowed { get; set; }
        [JsonPropertyName("allAppsVisible")]
        public bool? AllAppsVisible { get; set; }
    }
}
