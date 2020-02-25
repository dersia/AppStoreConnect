using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AppStoreConnect.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum CapabilityOptionKeys
    {
        XCODE_5,
        XCODE_6,
        COMPLETE_PROTECTION,
        PROTECTED_UNLESS_OPEN,
        PROTECTED_UNTIL_FIRST_USER_AUTH
    }
}
