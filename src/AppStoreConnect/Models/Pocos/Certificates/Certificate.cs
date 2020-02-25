using AppStoreConnect.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AppStoreConnect.Models.Pocos.Certificates
{
    public class Certificate
    {
        [JsonPropertyName("certificateContent")]
        public string? CertificateContent { get; set; }
        [JsonPropertyName("displayName")]
        public string? DisplayName { get; set; }
        [JsonPropertyName("expirationDate")]
        public string? ExpirationDate { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("platform")]
        [JsonConverter(typeof(JsonStringEnumMemberConverter))]
        public BundleIdPlatform? Platform { get; set; }
        [JsonPropertyName("serialNumber")]
        public string? SerialNumber { get; set; }
        [JsonPropertyName("certificateType")]
        [JsonConverter(typeof(JsonStringEnumMemberConverter))]
        public CertificateTypes? CertificateType { get; set; }
    }
}
