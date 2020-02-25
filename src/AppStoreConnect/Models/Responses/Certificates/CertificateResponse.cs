using AppStoreConnect.Models.Pocos.Certificates;
using AppStoreConnect.Models.Responses.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AppStoreConnect.Models.Responses.Certificates
{
    public class CertificateResponse : ApplicationResponse
    {
        [JsonPropertyName("data")]
        public CertificateInformation? CertificateInformation { get; set; }
        [JsonPropertyName("links")]
        public Dictionary<string, string>? Links { get; set; }
    }
}
