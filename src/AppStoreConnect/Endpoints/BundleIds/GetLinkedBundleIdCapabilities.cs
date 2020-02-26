using AppStoreConnect.Abstractions;
using AppStoreConnect.Constants;
using AppStoreConnect.Endpoints.Base;
using AppStoreConnect.Extensions;
using AppStoreConnect.Models.Filters.BundleIdCapabilities;
using AppStoreConnect.Models.Filters.BundleIds;
using AppStoreConnect.Models.Filters.Profiles;
using AppStoreConnect.Models.Responses.BundleIdCapabilities;
using AppStoreConnect.Models.Responses.BundleIds;
using AppStoreConnect.Models.Responses.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AppStoreConnect.Endpoints.BundleIds
{
    public partial class BundleIds : EndpointBase, IBundleIds
    {
        public async Task<ApplicationResponse> GetLinkedBundleIdCapabilities(string bundleId, GetLinkedBundleIdCapabilityFilters? filters = null)
        {
            var endpointUrl = $"{EndpointConstants.AppStoreConnectBaseUrl}/{EndpointConstants.Enpoints.BundleIds}/{bundleId}/{EndpointConstants.Enpoints.BundleIdCapabilities}";
            if (filters is { })
            {
                endpointUrl += "?filler=1";
                if (filters.LimitFilter is int limit)
                {
                    endpointUrl += $"{GetLinkedBundleIdCapabilityFilters.Keys.Limit}={limit}";
                }
                if(filters.FieldsBundleIdCapabilitiesFilter is { } fields && fields.Any())
                {
                    endpointUrl += $"{GetLinkedBundleIdCapabilityFilters.Keys.FieldsBundleIdCapabilities}={string.Join(',', fields)}";
                }
            }
            var result = await Client.GetAsync(endpointUrl);
            return result.ToPatternType() switch
            {
                { StatusCode: 200 } br => JsonSerializer.Deserialize<BundleIdCapabilitiesResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 400 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 403 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 404 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: _ } res => await InternalErrorResponse.CreateFromResponseMessage(res.Response),
            };
        }
    }
}
