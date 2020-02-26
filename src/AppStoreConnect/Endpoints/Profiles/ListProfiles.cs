using AppStoreConnect.Abstractions;
using AppStoreConnect.Constants;
using AppStoreConnect.Endpoints.Base;
using AppStoreConnect.Extensions;
using AppStoreConnect.Models.Filters.Devices;
using AppStoreConnect.Models.Filters.Profiles;
using AppStoreConnect.Models.Responses.Common;
using AppStoreConnect.Models.Responses.Devices;
using AppStoreConnect.Models.Responses.Profiles;
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
        public async Task<ApplicationResponse> ListProfiles(ListProfilesFilters? filters = null)
        {
            var endpointUrl = $"{EndpointConstants.AppStoreConnectBaseUrl}/{EndpointConstants.Enpoints.Profiles}";
            if (filters is { })
            {
                endpointUrl += "?filler=1";
                if (filters.NameFilter is { } names && names.Any())
                {
                    endpointUrl += $"{ListProfilesFilters.Keys.FilterNames}={string.Join(',', names)}";
                }
                if (filters.FieldsBundleIdsFilter is { } bundleIds && bundleIds.Any())
                {
                    endpointUrl += $"{ListProfilesFilters.Keys.FieldsBundleIds}={string.Join(',', bundleIds)}";
                }
                if (filters.FieldsDevicesFilter is { } devices && devices.Any())
                {
                    endpointUrl += $"{ListProfilesFilters.Keys.FieldsDevices}={string.Join(',', devices)}";
                }
                if (filters.IdFilter is { } ids && ids.Any())
                {
                    endpointUrl += $"{ListProfilesFilters.Keys.FilterIds}={string.Join(',', ids)}";
                }
                if (filters.LimitFilter is int limit)
                {
                    endpointUrl += $"{ListProfilesFilters.Keys.Limit}={limit}";
                }
                if (filters.FieldsCertificatesFilter is { } certificates && certificates.Any())
                {
                    endpointUrl += $"{ListProfilesFilters.Keys.FieldsCertificates}={string.Join(',', certificates)}";
                }
                if (filters.FieldsProfilesFilter is { } fields && fields.Any())
                {
                    endpointUrl += $"{ListProfilesFilters.Keys.FieldsProfiles}={string.Join(',', fields)}";
                }
                if (filters.LimitCertificatesFilter is int certifactesLimit)
                {
                    endpointUrl += $"{ListProfilesFilters.Keys.LimitCertificates}={certifactesLimit}";
                }
                if (filters.LimitDevicesFilter is int devicesLimit)
                {
                    endpointUrl += $"{ListProfilesFilters.Keys.LimitDevices}={devicesLimit}";
                }
                if (filters.ProfileStateFilter is { } profileStates && profileStates.Any())
                {
                    endpointUrl += $"{ListProfilesFilters.Keys.FilterProfileStates}={string.Join(',', profileStates)}";
                }
                if (filters.ProfileTypeFilter is { } profileTypes && profileTypes.Any())
                {
                    endpointUrl += $"{ListProfilesFilters.Keys.FilterProfileTypes}={string.Join(',', profileTypes)}";
                }
                if (filters.SortBy is { } sort && sort.Any())
                {
                    endpointUrl += $"{ListProfilesFilters.Keys.Sort}={string.Join(',', sort)}";
                }
                if (filters.IncludeFilter is { } include && include.Any())
                {
                    endpointUrl += $"{ListProfilesFilters.Keys.Include}={string.Join(',', include)}";
                }
            }
            var result = await Client.GetAsync(endpointUrl);
            return result.ToPatternType() switch
            {
                { StatusCode: 200 } br => JsonSerializer.Deserialize<ProfilesResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 400 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 403 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: _ } res => await InternalErrorResponse.CreateFromResponseMessage(res.Response),
            };
        }
    }
}
