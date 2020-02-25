using AppStoreConnect.Models.Pocos.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AppStoreConnect.Models.Requests.Users
{
    public class UserVisibleAppsLinkagesRequest
    {
        [JsonPropertyName("data")]
        public List<Data>? Apps { get; set; }
    }
}
