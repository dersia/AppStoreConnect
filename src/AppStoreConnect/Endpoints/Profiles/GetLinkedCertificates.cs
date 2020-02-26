using AppStoreConnect.Abstractions;
using AppStoreConnect.Constants;
using AppStoreConnect.Endpoints.Base;
using AppStoreConnect.Extensions;
using AppStoreConnect.Models.Filters.Certificates;
using AppStoreConnect.Models.Responses.Certificates;
using AppStoreConnect.Models.Responses.Common;
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
        public async Task<ApplicationResponse> GetLinkedCertificates(string profileId, GetLinkedCertificatesFilters? filters = null)
        {
            var endpointUrl = $"{EndpointConstants.AppStoreConnectBaseUrl}/{EndpointConstants.Enpoints.Profiles}/{profileId}/{EndpointConstants.Enpoints.Certificates}";
            if (filters is { })
            {
                endpointUrl += "?filler=1";
                if (filters.FieldsCertificatesFilter is { } certificates && certificates.Any())
                {
                    endpointUrl += $"{GetLinkedCertificatesFilters.Keys.FieldsCertificates}={string.Join(',', certificates)}";
                }
                if(filters.LimitFilter is int limit)
                {
                    endpointUrl += $"{GetLinkedCertificatesFilters.Keys.Limit}={limit}";
                }
            }
            var result = await Client.GetAsync(endpointUrl);
            return result.ToPatternType() switch
            {
                { StatusCode: 200 } br => JsonSerializer.Deserialize<CertificatesResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 400 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 403 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 404 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: _ } res => await InternalErrorResponse.CreateFromResponseMessage(res.Response),
            };
        }
    }
}
