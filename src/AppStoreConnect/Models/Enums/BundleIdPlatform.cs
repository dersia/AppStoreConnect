using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AppStoreConnect.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum BundleIdPlatform
    {
        IOS,
        MAC_OS,
        UNIVERSAL,
        SERVICES
    }
}
