using System;
using System.Collections.Generic;
using System.Text;

namespace AppStoreConnect.Models.Filters.Apps
{
    public class ListLinkedVisibleAppsFilters
    {
        public List<string>? AppsProjectionFilter { get; set; }
        public int? LimitVisibleAppsFilter { get; set; }

        public static class Keys
        {
            public const string FieldsApps = "&fields[apps]";
            public const string LimitVisibleApps = "&limit";
        }

        public static class FieldsApps
        {
            public const string BetaAppLocalizations = "betaAppLocalizations";
            public const string BetaAppReviewDetail = "betaAppReviewDetail";
            public const string BetaGroups = "betaGroups";
            public const string BetaLicenseAgreement = "betaLicenseAgreement";
            public const string BetaTesters = "betaTesters";
            public const string Builds = "builds";
            public const string BundleId = "bundleId";
            public const string Name = "name";
            public const string PreReleaseVersions = "preReleaseVersions";
            public const string PrimaryLocale = "primaryLocale";
            public const string Sku = "sku";
        }
    }
}
