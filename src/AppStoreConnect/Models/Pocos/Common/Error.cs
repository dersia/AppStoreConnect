using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AppStoreConnect.Models.Pocos.Common
{
    public class Error
    {
        [JsonPropertyName("code")]
        public string? Code { get; set; }
        [JsonPropertyName("status")]
        public string? Status { get; set; }
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        [JsonPropertyName("title")]
        public string? Title { get; set; }
        [JsonPropertyName("detail")]
        public string? Detail { get; set; }
        [JsonPropertyName("source")]
        public dynamic? Source { get; set; }
    }
}
