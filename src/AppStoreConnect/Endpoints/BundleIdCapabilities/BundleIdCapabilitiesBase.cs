using AppStoreConnect.Abstractions;
using AppStoreConnect.Endpoints.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppStoreConnect.Endpoints.BundleIdCapabilities
{
    public partial class BundleIdCapabilities : EndpointBase, IBundleIdCapabilities
    {
        public BundleIdCapabilities(string bearerToken) : base(bearerToken)
        {
        }
    }
}
