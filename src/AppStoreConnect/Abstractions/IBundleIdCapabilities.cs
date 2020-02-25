using AppStoreConnect.Models.Requests.BundleIdCapabilities;
using AppStoreConnect.Models.Responses.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppStoreConnect.Abstractions
{
    public interface IBundleIdCapabilities
    {
        Task<ApplicationResponse> DisableCapability(string id);
        Task<ApplicationResponse> EnableCapability(BundleIdCapabilityCreateRequest payload);
        Task<ApplicationResponse> ModifyCapability(string id, BundleIdCapabilityUpdateRequest payload);
    }
}
