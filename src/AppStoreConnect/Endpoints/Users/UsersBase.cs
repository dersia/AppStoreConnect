using AppStoreConnect.Abstractions;
using AppStoreConnect.Endpoints.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppStoreConnect.Endpoints.Users
{
    public partial class Users : EndpointBase, IUsers
    {
        public Users(string bearerToken) : base(bearerToken)
        {
        }
    }
}
