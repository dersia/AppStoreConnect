using AppStoreConnect.Abstractions;
using AppStoreConnect.Endpoints.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppStoreConnect.Endpoints.Certificates
{
    public partial class Certificates : EndpointBase, ICertificates
    {
        public Certificates(string bearerToken) : base(bearerToken)
        {
        }
    }
}
