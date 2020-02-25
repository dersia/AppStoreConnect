using AppStoreConnect.Models.Enums;
using AppStoreConnect.Models.Pocos.Certificates;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AppStoreConnect.Models.Requests.Certificates
{
    public class CertificateCreateRequest
    {
        [JsonPropertyName("data")]
        public CsrInformation? CsrInformation { get; set; }
    }
}
