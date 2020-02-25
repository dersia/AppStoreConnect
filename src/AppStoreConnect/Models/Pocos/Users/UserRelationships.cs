using AppStoreConnect.Models.Pocos.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AppStoreConnect.Models.Pocos.Users
{
    public class UserRelationships
    {
        [JsonPropertyName("visibleApps")]
        public Relationships? VisibleApps { get; set; }
    }
}
