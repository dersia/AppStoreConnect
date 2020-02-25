using AppStoreConnect.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AppStoreConnect.Models.Pocos.Devices
{
    public class Device
    {
        [JsonPropertyName("deviceClass")]
        [JsonConverter(typeof(JsonStringEnumMemberConverter))]
        public DeviceClass? DeviceClass { get; set; }
        [JsonPropertyName("model")]
        public string? Model { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("platform")]
        [JsonConverter(typeof(JsonStringEnumMemberConverter))]
        public BundleIdPlatform? Platform { get; set; }
        [JsonPropertyName("status")]
        [JsonConverter(typeof(JsonStringEnumMemberConverter))]
        public DeviceStatus? Status { get; set; }
        [JsonPropertyName("udid")]
        public string? Udid { get; set; }
        [JsonPropertyName("addedDate")]
        public string? AddedDate { get; set; }
    }
}
