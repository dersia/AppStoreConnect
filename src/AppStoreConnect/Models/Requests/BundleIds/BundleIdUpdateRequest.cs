using AppStoreConnect.Models.Pocos.BundleIds;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AppStoreConnect.Models.Requests.BundleIds
{
    public class BundleIdUpdateRequest
    {
        [JsonPropertyName("data")]
        public BundleIdInformation? BundleIdInformation { get; set; }
    }
}
