using AppStoreConnect.Models.Pocos.Common;
using AppStoreConnect.Models.Pocos.UserInvitations;
using AppStoreConnect.Models.Responses.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AppStoreConnect.Models.Responses.UserInvitations
{
    public class UserInvitationsResponse : ApplicationResponse
    {
        [JsonPropertyName("data")]
        public List<UserInvitationInformation>? UserInvitationInformations { get; set; }
        [JsonPropertyName("links")]
        public Dictionary<string, string>? Links { get; set; }
        [JsonPropertyName("meta")]
        public PagingInformation? PagingInformation { get; set; }
    }
}
