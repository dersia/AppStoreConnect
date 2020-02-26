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

namespace AppStoreConnect.Endpoints.Certificates
{
    public partial class Certificates : EndpointBase, ICertificates
    {
        public async Task<ApplicationResponse> ListCertificates(ListCertificatesFilters? filters = null)
        {
            var endpointUrl = $"{EndpointConstants.AppStoreConnectBaseUrl}/{EndpointConstants.Enpoints.Certificates}";
            if (filters is { })
            {
                endpointUrl += "?filler=1";
                if (filters.CertificateTypeFilter is { } certificateTypes && certificateTypes.Any())
                {
                    endpointUrl += $"{ListCertificatesFilters.Keys.FilterCertificateType}={string.Join(',', certificateTypes)}";
                }
                if (filters.DisplayNameFilter is { } displayNames && displayNames.Any())
                {
                    endpointUrl += $"{ListCertificatesFilters.Keys.FilterDisplayName}={string.Join(',', displayNames)}";
                }
                if (filters.FieldsCertificatesFilter is { } fields && fields.Any())
                {
                    endpointUrl += $"{ListCertificatesFilters.Keys.FieldsCertificates}={string.Join(',', fields)}";
                }
                if (filters.IdFilter is { } ids && ids.Any())
                {
                    endpointUrl += $"{ListCertificatesFilters.Keys.FilterId}={string.Join(',', ids)}";
                }
                if (filters.LimitFilter is int limit)
                {
                    endpointUrl += $"{ListCertificatesFilters.Keys.Limit}={limit}";
                }
                if (filters.SerialNumberFilter is { } serialNumbers && serialNumbers.Any())
                {
                    endpointUrl += $"{ListCertificatesFilters.Keys.FilterSerialNumber}={string.Join(',', serialNumbers)}";
                }
                if (filters.SortBy is { } sort && sort.Any())
                {
                    endpointUrl += $"{ListCertificatesFilters.Keys.Sort}={string.Join(',', sort)}";
                }
            }
            var result = await Client.GetAsync(endpointUrl);
            return result.ToPatternType() switch
            {
                { StatusCode: 200 } br => JsonSerializer.Deserialize<CertificatesResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 400 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 403 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: _ } res => await InternalErrorResponse.CreateFromResponseMessage(res.Response),
            };
        }
    }
}
