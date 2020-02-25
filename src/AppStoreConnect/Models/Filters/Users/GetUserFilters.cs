using System;
using System.Collections.Generic;
using System.Text;

namespace AppStoreConnect.Models.Filters.Users
{
    public class GetUserFilters
    {
        public List<string>? AppsProjectionFilter { get; set; }
        public List<string>? UserProjectionFilter { get; set; }
        public bool? IncludeVisibleAppsFilter { get; set; }
        public int? LimitVisibleAppsFilter { get; set; }

        public static class Keys
        {
            public const string FieldsApps = "&fields[apps]";
            public const string FieldsUsers = "&fields[users]";
            public const string Include = "&include";
            public const string LimitVisibleApps = "&limit[visibleApps]";
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

        public static class FieldsUsers
        {
            public const string AllAppsVisible = "allAppsVisible";
            public const string FirstName = "firstName";
            public const string LastName = "lastName";
            public const string ProvisioningAllowed = "provisioningAllowed";
            public const string Roles = "roles";
            public const string Username = "username";
            public const string VisibleApps = "visibleApps";
        }

        public static class Include
        {
            public const string VisibleApps = "visibleApps";
        }
    }
}
