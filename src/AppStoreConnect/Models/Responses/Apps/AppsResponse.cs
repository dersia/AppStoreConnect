using AppStoreConnect.Models.Pocos.Apps;
using AppStoreConnect.Models.Pocos.Common;
using AppStoreConnect.Models.Responses.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AppStoreConnect.Models.Responses.Apps
{
    public class AppsResponse : ApplicationResponse
    {
        [JsonPropertyName("data")]
        public List<AppInformation>? AppInformations { get; set; }
        [JsonPropertyName("links")]
        public Dictionary<string, string>? Links { get; set; }
        [JsonPropertyName("meta")]
        public PagingInformation? PagingInformation { get; set; }
        [JsonPropertyName("included")]
        public dynamic? IncludedResources { get; set; }
    }
}
