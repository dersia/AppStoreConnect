using AppStoreConnect.Models.Pocos.Devices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AppStoreConnect.Models.Requests.Devices
{
    public class DeviceUpdateRequest
    {
        [JsonPropertyName("data")]
        public DeviceInformation? DeviceInformation { get; set; }
    }
}
