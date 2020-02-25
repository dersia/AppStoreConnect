using AppStoreConnect.Models.Filters.BundleIdCapabilities;
using AppStoreConnect.Models.Filters.BundleIds;
using AppStoreConnect.Models.Filters.Profiles;
using AppStoreConnect.Models.Requests.BundleIds;
using AppStoreConnect.Models.Responses.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppStoreConnect.Abstractions
{
    public interface IBundleIds
    {
        Task<ApplicationResponse> DeleteBundleId(string id);
        Task<ApplicationResponse> GetBundleId(string id, GetBundleIdFilters? filters = null);
        Task<ApplicationResponse> GetLinkedBundleIdCapabilities(string bundleId, GetLinkedBundleIdCapabilityFilters? filters = null);
        Task<ApplicationResponse> GetLinkedBundleIdCapabilityIds(string bundleId, ListLinkedBundleIdCapabilitiesFilters? filters = null);
        Task<ApplicationResponse> GetLinkedProfileIds(string bundleId, ListLinkedProfilesFilters? filters = null);
        Task<ApplicationResponse> GetLinkedProfiles(string bundleId, GetLinkedProfileFilters? filters = null);
        Task<ApplicationResponse> ListBundleIds(ListBundleIdsFilters? filters = null);
        Task<ApplicationResponse> RegisterBundleId(BundleIdCreateRequest payload);
        Task<ApplicationResponse> UpdateBundleId(string id, BundleIdUpdateRequest payload);
    }
}
