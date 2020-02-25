using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AppStoreConnect.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum ProfileType
    {
        IOS_APP_DEVELOPMENT,
        IOS_APP_STORE,
        IOS_APP_ADHOC,
        IOS_APP_INHOUSE,
        MAC_APP_DEVELOPMENT,
        MAC_APP_STORE,
        MAC_APP_DIRECT,
        TVOS_APP_DEVELOPMENT,
        TVOS_APP_STORE,
        TVOS_APP_ADHOC,
        TVOS_APP_INHOUSE
    }
}
