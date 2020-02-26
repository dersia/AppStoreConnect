using AppStoreConnect.Abstractions;
using AppStoreConnect.Constants;
using AppStoreConnect.Endpoints.Base;
using AppStoreConnect.Extensions;
using AppStoreConnect.Models.Requests;
using AppStoreConnect.Models.Requests.Users;
using AppStoreConnect.Models.Responses;
using AppStoreConnect.Models.Responses.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AppStoreConnect.Endpoints.Users
{
    public partial class Users : EndpointBase, IUsers
    {
        public async Task<ApplicationResponse> RemoveVisibleAppsFromUser(string id, UserVisibleAppsLinkagesRequest userVisibleAppsLinkagesRequest)
        {
            var endpointUrl = $"{EndpointConstants.AppStoreConnectBaseUrl}/{EndpointConstants.Enpoints.Users}/{id}/{EndpointConstants.Enpoints.Relationships}/{EndpointConstants.Enpoints.VisibleApps}";
            var body = new StringContent(JsonSerializer.Serialize(userVisibleAppsLinkagesRequest));
            body.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(EndpointConstants.JsonContentType);
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(endpointUrl, UriKind.Absolute),
                Content = body
            };
            var result = await Client.SendAsync(request);
            return result.ToPatternType() switch
            {
                { StatusCode: 204 } br => new NoContentResponse(),
                { StatusCode: 403 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 404 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 409 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: _ } res => await InternalErrorResponse.CreateFromResponseMessage(res.Response),
            };
        }
    }
}
