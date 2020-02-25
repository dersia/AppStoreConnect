using AppStoreConnect.Abstractions;
using AppStoreConnect.Constants;
using AppStoreConnect.Endpoints.Base;
using AppStoreConnect.Extensions;
using AppStoreConnect.Models.Filters.Profiles;
using AppStoreConnect.Models.Responses.Common;
using AppStoreConnect.Models.Responses.Profiles;
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
        public async Task<ApplicationResponse> GetLinkedProfiles(string bundleId, GetLinkedProfileFilters? filters = null)
        {
            var endpointUrl = $"{EndpointConstants.AppStoreConnectBaseUrl}/{EndpointConstants.Enpoints.BundleIds}/{bundleId}/{EndpointConstants.Enpoints.Profiles}";
            if (filters is { })
            {
                endpointUrl += "?filler=1";
                if (filters.LimitProfilesFilter is int limit)
                {
                    endpointUrl += $"{GetLinkedProfileFilters.Keys.Limit}={limit}";
                }
                if (filters.FieldsProfilesFilter is { } fields && fields.Any())
                {
                    endpointUrl += $"{GetLinkedProfileFilters.Keys.FieldsProfiles}={string.Join(',', fields)}";
                }
            }
            var result = await Client.GetAsync(endpointUrl);
            return result.ToPatternType() switch
            {
                { StatusCode: 200 } br => JsonSerializer.Deserialize<ProfilesResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 400 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 403 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 404 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: _ } res => new InternalErrorResponse { Exception = new Exception(res.StatusCode.ToString()) },
            };
        }
    }
}
