using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AppStoreConnect.Models.Pocos.Common
{
    public class Relationships
    {
        [JsonPropertyName("data")]
        public List<Data>? Ids { get; set; }
        [JsonPropertyName("links")]
        public Dictionary<string, string>? Links { get; set; }
        [JsonPropertyName("meta")]
        public PagingInformation? PagingInformation { get; set; }
    }
}
