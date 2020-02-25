using AppStoreConnect.Models.Filters.Devices;
using AppStoreConnect.Models.Requests.Devices;
using AppStoreConnect.Models.Responses.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppStoreConnect.Abstractions
{
    public interface IDevices
    {
        Task<ApplicationResponse> GetDevice(string id, GetDeviceFilters? filters = null);
        Task<ApplicationResponse> ListDevices(ListDevicesFilters? filters = null);
        Task<ApplicationResponse> RegisterDevice(DeviceCreateRequest payload);
        Task<ApplicationResponse> UpdateDevice(string id, DeviceUpdateRequest payload);
    }
}
