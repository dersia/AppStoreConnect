using AppStoreConnect.Models.Pocos.BundleIds;
using AppStoreConnect.Models.Pocos.Common;
using AppStoreConnect.Models.Responses.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AppStoreConnect.Models.Responses.BundleIds
{
    public class BundleIdsResponse : ApplicationResponse
    {
        [JsonPropertyName("data")]
        public List<BundleIdInformation>? BundleIdInformations { get; set; }
        [JsonPropertyName("links")]
        public Dictionary<string, string>? Links { get; set; }
        [JsonPropertyName("meta")]
        public PagingInformation? PagingInformation { get; set; }
        [JsonPropertyName("included")]
        public dynamic? Included { get; set; }
    }
}
