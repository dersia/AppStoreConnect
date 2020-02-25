using AppStoreConnect.Models.Pocos.Profiles;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AppStoreConnect.Models.Requests.Profiles
{
    public class ProfileCreateRequest
    {
        [JsonPropertyName("data")]
        public ProfileInformation? ProfileInformation { get; set; }
    }
}
