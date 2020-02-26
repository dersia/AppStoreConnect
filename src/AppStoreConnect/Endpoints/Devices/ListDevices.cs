using AppStoreConnect.Abstractions;
using AppStoreConnect.Constants;
using AppStoreConnect.Endpoints.Base;
using AppStoreConnect.Extensions;
using AppStoreConnect.Models.Filters.Devices;
using AppStoreConnect.Models.Responses.Common;
using AppStoreConnect.Models.Responses.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AppStoreConnect.Endpoints.Devices
{
    public partial class Devices : EndpointBase, IDevices
    {
        public async Task<ApplicationResponse> ListDevices(ListDevicesFilters? filters = null)
        {
            var endpointUrl = $"{EndpointConstants.AppStoreConnectBaseUrl}/{EndpointConstants.Enpoints.Devices}";
            if (filters is { })
            {
                endpointUrl += "?filler=1";
                if (filters.NameFilter is { } names && names.Any())
                {
                    endpointUrl += $"{ListDevicesFilters.Keys.FilterName}={string.Join(',', names)}";
                }
                if (filters.PlatformFilter is { } platforms && platforms.Any())
                {
                    endpointUrl += $"{ListDevicesFilters.Keys.FilterPlatform}={string.Join(',', platforms)}";
                }
                if (filters.FieldsDevicesFilter is { } fields && fields.Any())
                {
                    endpointUrl += $"{ListDevicesFilters.Keys.FieldsDevices}={string.Join(',', fields)}";
                }
                if (filters.IdFilter is { } ids && ids.Any())
                {
                    endpointUrl += $"{ListDevicesFilters.Keys.FilterId}={string.Join(',', ids)}";
                }
                if (filters.LimitFilter is int limit)
                {
                    endpointUrl += $"{ListDevicesFilters.Keys.Limit}={limit}";
                }
                if (filters.StatusFilter is { } statuus && statuus.Any())
                {
                    endpointUrl += $"{ListDevicesFilters.Keys.FilterStatus}={string.Join(',', statuus)}";
                }
                if (filters.UdidFilter is { } udids && udids.Any())
                {
                    endpointUrl += $"{ListDevicesFilters.Keys.FilterUdid}={string.Join(',', udids)}";
                }
                if (filters.SortBy is { } sort && sort.Any())
                {
                    endpointUrl += $"{ListDevicesFilters.Keys.Sort}={string.Join(',', sort)}";
                }
            }
            var result = await Client.GetAsync(endpointUrl);
            return result.ToPatternType() switch
            {
                { StatusCode: 200 } br => JsonSerializer.Deserialize<DevicesResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 400 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 403 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: _ } res => await InternalErrorResponse.CreateFromResponseMessage(res.Response),
            };
        }
    }
}
