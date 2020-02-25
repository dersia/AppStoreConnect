using AppStoreConnect.Models.Pocos.Apps;
using AppStoreConnect.Models.Pocos.Common;
using AppStoreConnect.Models.Pocos.Users;
using AppStoreConnect.Models.Responses.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AppStoreConnect.Models.Responses.Users
{
    public class UsersResponse : ApplicationResponse
    {
        [JsonPropertyName("data")]
        public List<UserInformation>? Users { get; set; }
        [JsonPropertyName("links")]
        public Dictionary<string, string>? Links { get; set; }
        [JsonPropertyName("meta")]
        public PagingInformation? PagingInformation { get; set; }
        [JsonPropertyName("included")]
        public List<AppInformation>? IncludedApps { get; set; }
    }
}
