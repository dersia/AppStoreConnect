using AppStoreConnect.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AppStoreConnect.Models.Pocos.Certificates
{
    public class CsrInformation
    {
        [JsonPropertyName("attributes")]
        public Csr? Csr { get; set; }
        [JsonPropertyName("type")]
        public ResourceTypes? Type { get; set; }
    }
}
