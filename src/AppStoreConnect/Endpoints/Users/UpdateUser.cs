using AppStoreConnect.Abstractions;
using AppStoreConnect.Constants;
using AppStoreConnect.Endpoints.Base;
using AppStoreConnect.Extensions;
using AppStoreConnect.Models.Requests;
using AppStoreConnect.Models.Requests.Users;
using AppStoreConnect.Models.Responses;
using AppStoreConnect.Models.Responses.Common;
using AppStoreConnect.Models.Responses.Users;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AppStoreConnect.Endpoints.Users
{
    public partial class Users : EndpointBase, IUsers
    {
        public async Task<ApplicationResponse> UpdateUser(string id, UserUpdateRequest payload)
        {
            var endpointUrl = $"{EndpointConstants.AppStoreConnectBaseUrl}/{EndpointConstants.Enpoints.Users}/{id}";
            var body = new StringContent(JsonSerializer.Serialize(payload, new JsonSerializerOptions { IgnoreNullValues = true }));
            body.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(EndpointConstants.JsonContentType);
            var result = await Client.PatchAsync(endpointUrl, body);
            return result.ToPatternType() switch
            {
                { StatusCode: 200 } br => JsonSerializer.Deserialize<UserResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 400 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 403 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 404 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 409 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: _ } res => await InternalErrorResponse.CreateFromResponseMessage(res.Response),
            }; ;
        }
    }
}
