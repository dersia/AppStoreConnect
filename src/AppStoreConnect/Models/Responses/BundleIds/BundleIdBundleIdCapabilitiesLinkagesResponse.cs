using AppStoreConnect.Models.Pocos.Common;
using AppStoreConnect.Models.Responses.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AppStoreConnect.Models.Responses.BundleIds
{
    public class BundleIdBundleIdCapabilitiesLinkagesResponse : ApplicationResponse
    {
        [JsonPropertyName("data")]
        public List<Data>? BundleIdCapabilityIds { get; set; }
        [JsonPropertyName("links")]
        public Dictionary<string, string>? Links { get; set; }
        [JsonPropertyName("meta")]
        public PagingInformation? PagingInformation { get; set; }
    }
}
