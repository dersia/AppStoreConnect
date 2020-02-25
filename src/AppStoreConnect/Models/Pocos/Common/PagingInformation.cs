using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AppStoreConnect.Models.Pocos.Common
{
    public class PagingInformation
    {
        [JsonPropertyName("paging")]
        public Paging? Paging { get; set; }
    }
}
