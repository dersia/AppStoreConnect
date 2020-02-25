using AppStoreConnect.Models.Pocos.BundleIds;
using AppStoreConnect.Models.Responses.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AppStoreConnect.Models.Responses.BundleIds
{
    public class BundleIdResponse : ApplicationResponse
    {
        [JsonPropertyName("data")]
        public BundleIdInformation? BundleIdInformation { get; set; }
        [JsonPropertyName("links")]
        public Dictionary<string, string>? Links { get; set; }
        [JsonPropertyName("included")]
        public dynamic? Included { get; set; }
    }
}
