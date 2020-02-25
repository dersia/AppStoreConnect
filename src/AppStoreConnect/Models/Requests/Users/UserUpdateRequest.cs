using AppStoreConnect.Models.Pocos.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AppStoreConnect.Models.Requests.Users
{
    public class UserUpdateRequest
    {
        [JsonPropertyName("data")]
        public UserInformation? UserInformation { get; set; }
    }
}
