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

namespace AppStoreConnect.Endpoints.Profiles
{
    public partial class Profiles : EndpointBase, IProfiles
    {
        public async Task<ApplicationResponse> GetLinkedDevices(string profileId, GetLinkedDevicesFilters? filters = null)
        {
            var endpointUrl = $"{EndpointConstants.AppStoreConnectBaseUrl}/{EndpointConstants.Enpoints.Profiles}/{profileId}/{EndpointConstants.Enpoints.Devices}";
            if (filters is { })
            {
                endpointUrl += "?filler=1";
                if (filters.FieldsDevicesFilter is { } devices && devices.Any())
                {
                    endpointUrl += $"{GetLinkedDevicesFilters.Keys.FieldsDevices}={string.Join(',', devices)}";
                }
                if (filters.LimitFilter is int limit)
                {
                    endpointUrl += $"{GetLinkedDevicesFilters.Keys.Limit}={limit}";
                }
            }
            var result = await Client.GetAsync(endpointUrl);
            return result.ToPatternType() switch
            {
                { StatusCode: 200 } br => JsonSerializer.Deserialize<DevicesResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 400 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 403 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 404 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: _ } res => await InternalErrorResponse.CreateFromResponseMessage(res.Response),
            };
        }
    }
}
