using AppStoreConnect.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppStoreConnect.Models.Filters.UserInvitations
{
    public class ListUserInvitationsFilters
    {
        public List<string>? AppsProjectionFilter { get; set; }
        public List<string>? UserInvitationsProjectionFilter { get; set; }
        public bool? IncludeVisibleAppsFilter { get; set; }
        public int? LimitUsersFilter { get; set; }
        public List<string>? SortBy { get; set; }
        public List<UserRoles>? RolesFilter { get; set; }
        public List<string>? VisibleAppsFilter { get; set; }
        public List<string>? EmailsFilter { get; set; }
        public int? LimitVisibleAppsFilter { get; set; }


        public static class Keys
        {
            public const string FieldsApps = "&fields[apps]";
            public const string FieldsUsers = "&fields[userInvitations]";
            public const string Include = "&include";
            public const string Limit = "&limit";
            public const string Sort = "&sort";
            public const string FilterRoles = "&filter[roles]";
            public const string FilterVisibleApps = "&filter[visibleApps]";
            public const string FilterEmail = "&filter[email]";
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
            public const string Email = "email";
            public const string ProvisioningAllowed = "provisioningAllowed";
            public const string Roles = "roles";
            public const string ExpirationDate = "expirationDate";
            public const string VisibleApps = "visibleApps";
        }

        public static class Include
        {
            public const string VisibleApps = "visibleApps";
        }

        public static class Sort
        {
            public const string LastName = "lastName";
            public const string LastNameDesc = "-lastName";
            public const string Email = "email";
            public const string EmailDesc = "-email";
        }
    }
}
