using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AppStoreConnect.Models.Pocos.Common
{
    public class Paging
    {
        [JsonPropertyName("total")]
        public int? Total { get; set; }
        [JsonPropertyName("limit")]
        public int? Limit { get; set; }
    }
}
