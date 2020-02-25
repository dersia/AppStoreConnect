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
        public async Task<ApplicationResponse> GetProfile(string id, GetProfileFilters? filters = null)
        {
            var endpointUrl = $"{EndpointConstants.AppStoreConnectBaseUrl}/{EndpointConstants.Enpoints.Profiles}/{id}";
            if (filters is { })
            {
                endpointUrl += "?filler=1";
                if (filters.FieldsBundleIdsFilter is { } bundleIds && bundleIds.Any())
                {
                    endpointUrl += $"{GetProfileFilters.Keys.FieldsBundleIds}={string.Join(',', bundleIds)}";
                }
                if (filters.FieldsDevicesFilter is { } devices && devices.Any())
                {
                    endpointUrl += $"{GetProfileFilters.Keys.FieldsDevices}={string.Join(',', devices)}";
                }
                if (filters.FieldsCertificatesFilter is { } certificates && certificates.Any())
                {
                    endpointUrl += $"{GetProfileFilters.Keys.FieldsCertificates}={string.Join(',', certificates)}";
                }
                if (filters.FieldsProfilesFilter is { } fields && fields.Any())
                {
                    endpointUrl += $"{GetProfileFilters.Keys.FieldsProfiles}={string.Join(',', fields)}";
                }
                if (filters.LimitCertificatesFilter is int certifactesLimit)
                {
                    endpointUrl += $"{GetProfileFilters.Keys.LimitCertificates}={certifactesLimit}";
                }
                if (filters.LimitDevicesFilter is int devicesLimit)
                {
                    endpointUrl += $"{GetProfileFilters.Keys.LimitDevices}={devicesLimit}";
                }
                if (filters.IncludeFilter is { } include && include.Any())
                {
                    endpointUrl += $"{GetProfileFilters.Keys.Include}={string.Join(',', include)}";
                }
            }
            var result = await Client.GetAsync(endpointUrl);
            return result.ToPatternType() switch
            {
                { StatusCode: 200 } br => JsonSerializer.Deserialize<ProfileResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 400 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 403 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 404 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: _ } res => new InternalErrorResponse { Exception = new Exception(res.StatusCode.ToString()) },
            };
        }
    }
}
