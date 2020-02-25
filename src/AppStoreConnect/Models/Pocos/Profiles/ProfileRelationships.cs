using AppStoreConnect.Models.Pocos.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AppStoreConnect.Models.Pocos.Profiles
{
    public class ProfileRelationships
    {
        [JsonPropertyName("certificates")]
        public Relationships? CertificateIds { get; set; }
        [JsonPropertyName("devices")]
        public Relationships? Devices { get; set; }
        [JsonPropertyName("bundleId")]
        public Relationship? BundleId { get; set; }
    }
}
