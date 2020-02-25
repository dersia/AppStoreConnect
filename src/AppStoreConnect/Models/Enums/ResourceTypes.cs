using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AppStoreConnect.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum ResourceTypes
    {
        apps,
        users,
        userInvitations,
        profiles,
        certificates,
        devices,
        bundleIds,
        betaLicenseAgreements,
        preReleaseVersions,
        betaAppLocalizations,
        betaGroups,
        betaTesters,
        builds,
        betaAppReviewDetails,
        bundleIdCapabilities
    }
}
