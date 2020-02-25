using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AppStoreConnect.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum CertificateTypes
    {
        IOS_DEVELOPMENT,
        IOS_DISTRIBUTION,
        MAC_APP_DISTRIBUTION,
        MAC_INSTALLER_DISTRIBUTION,
        MAC_APP_DEVELOPMENT,
        DEVELOPER_ID_KEXT,
        DEVELOPER_ID_APPLICATION, 
        DEVELOPMENT
    }
}
