using AppStoreConnect.Models.Pocos.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AppStoreConnect.Models.Pocos.Apps
{
    public class AppRelationships
    {
        [JsonPropertyName("betaLicenseAgreement")]
        public Relationship? BetaLicenseAgreement { get; set; }
        [JsonPropertyName("preReleaseVersions")]
        public Relationships? PreReleaseVersions { get; set; }
        [JsonPropertyName("betaAppLocalizations")]
        public Relationships? BetaAppLocalizations { get; set; }
        [JsonPropertyName("betaGroups")]
        public Relationships? BetaGroups { get; set; }
        [JsonPropertyName("betaTesters")]
        public Relationships? BetaTesters { get; set; }
        [JsonPropertyName("builds")]
        public Relationships? Builds { get; set; }
        [JsonPropertyName("betaAppReviewDetail")]
        public Relationship? BetaAppReviewDetail { get; set; }
    }
}
