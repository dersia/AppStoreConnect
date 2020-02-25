using AppStoreConnect.Models.Filters.BundleIds;
using AppStoreConnect.Models.Filters.Certificates;
using AppStoreConnect.Models.Filters.Devices;
using AppStoreConnect.Models.Filters.Profiles;
using AppStoreConnect.Models.Requests.Profiles;
using AppStoreConnect.Models.Responses.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppStoreConnect.Abstractions
{
    public interface IProfiles
    {
        Task<ApplicationResponse> CreateProfile(ProfileCreateRequest payload);
        Task<ApplicationResponse> DeleteProfile(string id);
        Task<ApplicationResponse> GetLinkedBundleId(string profileId, GetLinkedBundleIdFilter? filters = null);
        Task<ApplicationResponse> GetLinkedBundleIdId(string profileId);
        Task<ApplicationResponse> GetLinkedCertificateIds(string profileId, GetLinkedCertificateIdsFilters? filters = null);
        Task<ApplicationResponse> GetLinkedCertificates(string profileId, GetLinkedCertificatesFilters? filters = null);
        Task<ApplicationResponse> GetLinkedDeviceIds(string profileId, GetLinkedDeviceIdsFilters? filters = null);
        Task<ApplicationResponse> GetLinkedDevices(string profileId, GetLinkedDevicesFilters? filters = null);
        Task<ApplicationResponse> GetProfile(string id, GetProfileFilters? filters = null);
        Task<ApplicationResponse> ListProfiles(ListProfilesFilters? filters = null);

    }
}
