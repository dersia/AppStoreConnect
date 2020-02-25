using AppStoreConnect.Models.Pocos.Profiles;
using AppStoreConnect.Models.Responses.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AppStoreConnect.Models.Responses.Profiles
{
    public class ProfileResponse : ApplicationResponse
    {
        [JsonPropertyName("data")]
        public ProfileInformation? ProfileInformation { get; set; }
        [JsonPropertyName("links")]
        public Dictionary<string, string>? Links { get; set; }
        [JsonPropertyName("included")]
        public dynamic? Included { get; set; }
    }
}
