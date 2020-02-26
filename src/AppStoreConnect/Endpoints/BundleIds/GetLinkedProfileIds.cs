using AppStoreConnect.Abstractions;
using AppStoreConnect.Constants;
using AppStoreConnect.Endpoints.Base;
using AppStoreConnect.Extensions;
using AppStoreConnect.Models.Filters.BundleIds;
using AppStoreConnect.Models.Filters.Profiles;
using AppStoreConnect.Models.Responses.BundleIds;
using AppStoreConnect.Models.Responses.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AppStoreConnect.Endpoints.BundleIds
{
    public partial class BundleIds : EndpointBase, IBundleIds
    {
        public async Task<ApplicationResponse> GetLinkedProfileIds(string bundleId, ListLinkedProfilesFilters? filters = null)
        {
            var endpointUrl = $"{EndpointConstants.AppStoreConnectBaseUrl}/{EndpointConstants.Enpoints.BundleIds}/{bundleId}/{EndpointConstants.Enpoints.Relationships}/{EndpointConstants.Enpoints.Profiles}";
            if (filters is { })
            {
                endpointUrl += "?filler=1";
                if (filters.LimitProfilesFilter is int limit)
                {
                    endpointUrl += $"{ListLinkedProfilesFilters.Keys.Limit}={limit}";
                }
            }
            var result = await Client.GetAsync(endpointUrl);
            return result.ToPatternType() switch
            {
                { StatusCode: 200 } br => JsonSerializer.Deserialize<BundleIdProfilesLinkagesResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 400 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 403 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 404 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: _ } res => await InternalErrorResponse.CreateFromResponseMessage(res.Response),
            };
        }
    }
}
