using AppStoreConnect.Abstractions;
using AppStoreConnect.Endpoints.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppStoreConnect.Endpoints.UserInvitations
{
    public partial class UserInvitations : EndpointBase, IUserInvitations
    {
        public UserInvitations(string bearerToken) : base(bearerToken)
        {
        }
    }
}
