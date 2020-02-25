using AppStoreConnect.Models.Pocos.Common;
using AppStoreConnect.Models.Pocos.Devices;
using AppStoreConnect.Models.Responses.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AppStoreConnect.Models.Responses.Devices
{
    public class DevicesResponse : ApplicationResponse
    {
        [JsonPropertyName("data")]
        public List<DeviceInformation>? Devices { get; set; }
        [JsonPropertyName("links")]
        public Dictionary<string, string>? Links { get; set; }
        [JsonPropertyName("meta")]
        public PagingInformation? PagingInformation { get; set; }
    }
}
