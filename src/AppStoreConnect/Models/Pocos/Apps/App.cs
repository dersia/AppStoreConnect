using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AppStoreConnect.Models.Pocos.Apps
{
    public class App
    {
        [JsonPropertyName("bundleId")]
        public string? BundleId { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("primaryLocale")]
        public string? PrimaryLocale { get; set; }
        [JsonPropertyName("sku")]
        public string? Sku { get; set; }
    }
}
