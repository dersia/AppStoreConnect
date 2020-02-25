using AppStoreConnect.Abstractions;
using AppStoreConnect.Endpoints.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppStoreConnect.Endpoints.Devices
{
    public partial class Devices : EndpointBase, IDevices
    {
        public Devices(string bearerToken) : base(bearerToken)
        {
        }
    }
}
