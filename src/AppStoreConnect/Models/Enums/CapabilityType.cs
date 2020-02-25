using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AppStoreConnect.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum CapabilityType
    {
        ICLOUD,
        IN_APP_PURCHASE,
        GAME_CENTER,
        PUSH_NOTIFICATIONS,
        WALLET,
        INTER_APP_AUDIO,
        MAPS,
        ASSOCIATED_DOMAINS,
        PERSONAL_VPN,
        APP_GROUPS,
        HEALTHKIT,
        HOMEKIT,
        WIRELESS_ACCESSORY_CONFIGURATION,
        APPLE_PAY,
        DATA_PROTECTION,
        SIRIKIT,
        NETWORK_EXTENSIONS,
        MULTIPATH,
        HOT_SPOT,
        NFC_TAG_READING,
        CLASSKIT,
        AUTOFILL_CREDENTIAL_PROVIDER,
        ACCESS_WIFI_INFORMATION,
        FONT_INSTALLATION,

    }
}
