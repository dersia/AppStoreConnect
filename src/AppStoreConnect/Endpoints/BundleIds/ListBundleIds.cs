using AppStoreConnect.Abstractions;
using AppStoreConnect.Constants;
using AppStoreConnect.Endpoints.Base;
using AppStoreConnect.Extensions;
using AppStoreConnect.Models.Filters.BundleIds;
using AppStoreConnect.Models.Requests;
using AppStoreConnect.Models.Responses;
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
        public async Task<ApplicationResponse> ListBundleIds(ListBundleIdsFilters? filters = null)
        {
            var endpointUrl = $"{EndpointConstants.AppStoreConnectBaseUrl}/{EndpointConstants.Enpoints.BundleIds}";
            if (filters is { })
            {
                endpointUrl += "?filler=1";
                if (filters.FieldsBundleIdsFilter is { } bundleIds && bundleIds.Any())
                {
                    endpointUrl += $"{ListBundleIdsFilters.Keys.FieldsBundleIds}={string.Join(',', bundleIds)}";
                }
                if (filters.FieldsCapabilitiesFilter is { } capabitilies && capabitilies.Any())
                {
                    endpointUrl += $"{ListBundleIdsFilters.Keys.FieldsCapabilities}={string.Join(',', capabitilies)}";
                }
                if (filters.FieldsProfilesFilter is { } profiles && profiles.Any())
                {
                    endpointUrl += $"{ListBundleIdsFilters.Keys.FieldsProfiles}={string.Join(',', profiles)}";
                }
                if (filters.IncludeFilter is { } includes && includes.Any())
                {
                    endpointUrl += $"{ListBundleIdsFilters.Keys.Include}={string.Join(',', includes)}";
                }
                if (filters.LimitBundleIdsFilter is { } limitBundleIds)
                {
                    endpointUrl += $"{ListBundleIdsFilters.Keys.Limit}={limitBundleIds}";
                }
                if (filters.LimitProfilesFilter is int profilesLimit)
                {
                    endpointUrl += $"{ListBundleIdsFilters.Keys.LimitProfiles}={profilesLimit}";
                }
                if (filters.NameFilter is { } names && names.Any())
                {
                    endpointUrl += $"{ListBundleIdsFilters.Keys.FilterName}={string.Join(',', names)}";
                }
                if (filters.SortBy is { } sort && sort.Any())
                {
                    endpointUrl += $"{ListBundleIdsFilters.Keys.Sort}={string.Join(',', sort)}";
                }
                if (filters.PlatformFilter is { } platforms && platforms.Any())
                {
                    endpointUrl += $"{ListBundleIdsFilters.Keys.FilterPlatform}={string.Join(',', platforms)}";
                }
                if (filters.IdentifierFilter is { } indentifiers && indentifiers.Any())
                {
                    endpointUrl += $"{ListBundleIdsFilters.Keys.FilterIdentifier}={string.Join(',', indentifiers)}";
                }
                if (filters.IdFilter is { } ids && ids.Any())
                {
                    endpointUrl += $"{ListBundleIdsFilters.Keys.FilterId}={string.Join(',', ids)}";
                }
                if (filters.SeedIdFilter is { } seedIds && seedIds.Any())
                {
                    endpointUrl += $"{ListBundleIdsFilters.Keys.FilterSeedId}={string.Join(',', seedIds)}";
                }
            }
            var result = await Client.GetAsync(endpointUrl);
            return result.ToPatternType() switch
            {
                { StatusCode: 200 } br => JsonSerializer.Deserialize<BundleIdsResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 400 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 403 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: _ } res => await InternalErrorResponse.CreateFromResponseMessage(res.Response),
            };
        }
    }
}
