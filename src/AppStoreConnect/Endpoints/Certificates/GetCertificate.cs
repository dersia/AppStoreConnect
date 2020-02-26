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
        public async Task<ApplicationResponse> GetCertificate(string id, GetCertificateFilters? filters = null)
        {
            var endpointUrl = $"{EndpointConstants.AppStoreConnectBaseUrl}/{EndpointConstants.Enpoints.Certificates}/{id}";
            if (filters is { })
            {
                endpointUrl += "?filler=1";
                if (filters.FieldsCertificatesFilter is { } fields && fields.Any())
                {
                    endpointUrl += $"{GetCertificateFilters.Keys.FieldsCertificates}={string.Join(',', fields)}";
                }
            }
            var result = await Client.GetAsync(endpointUrl);
            return result.ToPatternType() switch
            {
                { StatusCode: 200 } br => JsonSerializer.Deserialize<CertificateResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 400 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 403 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 404 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: _ } res => await InternalErrorResponse.CreateFromResponseMessage(res.Response),
            };
        }
    }
}
