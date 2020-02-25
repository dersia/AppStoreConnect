using AppStoreConnect.Models.Pocos.Common;
using AppStoreConnect.Models.Responses.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AppStoreConnect.Models.Responses.Profiles
{
    public class ProfileBundleIdLinkageResponse : ApplicationResponse
    {
        [JsonPropertyName("data")]
        public Data BundleIdId { get; set; }
        [JsonPropertyName("links")]
        public Dictionary<string, string>? Links { get; set; }
    }
}
