using AppStoreConnect.Models.Pocos.UserInvitations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AppStoreConnect.Models.Requests.UserInvitations
{
    public class UserInvitationCreateRequest
    {
        [JsonPropertyName("data")]
        public UserInvitationInformation? UserInvitationInformation { get; set; }
    }
}
