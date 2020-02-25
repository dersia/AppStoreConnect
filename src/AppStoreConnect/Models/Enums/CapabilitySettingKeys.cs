using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AppStoreConnect.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum CapabilitySettingKeys
    {
        ICLOUD_VERSION,
        DATA_PROTECTION_PERMISSION_LEVEL
    }
}
