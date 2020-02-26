using AppStoreConnect.Abstractions;
using AppStoreConnect.Constants;
using AppStoreConnect.Endpoints.Base;
using AppStoreConnect.Extensions;
using AppStoreConnect.Models.Filters.BundleIds;
using AppStoreConnect.Models.Requests;
using AppStoreConnect.Models.Requests.BundleIds;
using AppStoreConnect.Models.Responses;
using AppStoreConnect.Models.Responses.BundleIds;
using AppStoreConnect.Models.Responses.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AppStoreConnect.Endpoints.BundleIds
{
    public partial class BundleIds : EndpointBase, IBundleIds
    {
        public async Task<ApplicationResponse> GetBundleId(string id, GetBundleIdFilters? filters = null)
        {
            var endpointUrl = $"{EndpointConstants.AppStoreConnectBaseUrl}/{EndpointConstants.Enpoints.BundleIds}/{id}";
            if (filters is { })
            {
                endpointUrl += "?filler=1";
                if (filters.FieldsBundleIdsFilter is { } bundleIds && bundleIds.Any())
                {
                    endpointUrl += $"{GetBundleIdFilters.Keys.FieldsBundleIds}={string.Join(',', bundleIds)}";
                }
                if (filters.FieldsCapabilitiesFilter is { } capabitilies && capabitilies.Any())
                {
                    endpointUrl += $"{GetBundleIdFilters.Keys.FieldsCapabilities}={string.Join(',', capabitilies)}";
                }
                if (filters.FieldsProfilesFilter is { } profiles && profiles.Any())
                {
                    endpointUrl += $"{GetBundleIdFilters.Keys.FieldsProfiles}={string.Join(',', profiles)}";
                }
                if (filters.IncludeFilter is { } includes && includes.Any())
                {
                    endpointUrl += $"{GetBundleIdFilters.Keys.Include}={string.Join(',', includes)}";
                }
                if (filters.LimitProfilesFilter is int profilesLimit)
                {
                    endpointUrl += $"{GetBundleIdFilters.Keys.LimitProfiles}={profilesLimit}";
                }
            }
            var result = await Client.GetAsync(endpointUrl);
            return result.ToPatternType() switch
            {
                { StatusCode: 200 } br => JsonSerializer.Deserialize<BundleIdResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 400 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 403 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 404 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: _ } res => await InternalErrorResponse.CreateFromResponseMessage(res.Response),
            };
        }
    }
}
