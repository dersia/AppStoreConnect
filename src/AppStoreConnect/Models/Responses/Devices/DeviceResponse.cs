using AppStoreConnect.Models.Pocos.Devices;
using AppStoreConnect.Models.Responses.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AppStoreConnect.Models.Responses.Devices
{
    public class DeviceResponse : ApplicationResponse
    {
        [JsonPropertyName("data")]
        public DeviceInformation? DeviceInformation { get; set; }
        [JsonPropertyName("links")]
        public Dictionary<string, string>? Links { get; set; }
    }
}
