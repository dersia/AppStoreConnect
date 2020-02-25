using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AppStoreConnect.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum UserRoles
    {
        ADMIN,
        FINANCE,
        TECHNICAL,
        SALES,
        MARKETING,
        DEVELOPER,
        ACCOUNT_HOLDER,
        READ_ONLY,
        APP_MANAGER,
        ACCESS_TO_REPORTS,
        CUSTOMER_SUPPORT
    }
}
