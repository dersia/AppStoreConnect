using AppStoreConnect.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AppStoreConnect.Models.Pocos.Certificates
{
    public class Csr
    {
        [JsonPropertyName("certificateType")]
        public CertificateTypes? CertificateType { get; set; }
        [JsonPropertyName("csrContent")]
        public string? CsrContent { get; set; }
    }
}
