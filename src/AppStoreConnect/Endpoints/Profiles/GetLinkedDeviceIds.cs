using AppStoreConnect.Abstractions;
using AppStoreConnect.Constants;
using AppStoreConnect.Endpoints.Base;
using AppStoreConnect.Extensions;
using AppStoreConnect.Models.Filters.Devices;
using AppStoreConnect.Models.Responses.Common;
using AppStoreConnect.Models.Responses.Profiles;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AppStoreConnect.Endpoints.Profiles
{
    public partial class Profiles : EndpointBase, IProfiles
    {
        public async Task<ApplicationResponse> GetLinkedDeviceIds(string profileId, GetLinkedDeviceIdsFilters? filters = null)
        {
            var endpointUrl = $"{EndpointConstants.AppStoreConnectBaseUrl}/{EndpointConstants.Enpoints.Profiles}/{profileId}/{EndpointConstants.Enpoints.Relationships}/{EndpointConstants.Enpoints.Devices}";
            if (filters is { })
            {
                endpointUrl += "?filler=1";
                if (filters.LimitFilter is int limit)
                {
                    endpointUrl += $"{GetLinkedDeviceIdsFilters.Keys.Limit}={limit}";
                }
            }
            var result = await Client.GetAsync(endpointUrl);
            return result.ToPatternType() switch
            {
                { StatusCode: 200 } br => JsonSerializer.Deserialize<ProfileDevicesLinkagesResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 400 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 403 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 404 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: _ } res => new InternalErrorResponse { Exception = new Exception(res.StatusCode.ToString()) },
            };
        }
    }
}
