using AppStoreConnect.Abstractions;
using AppStoreConnect.Constants;
using AppStoreConnect.Endpoints.Base;
using AppStoreConnect.Extensions;
using AppStoreConnect.Models.Requests;
using AppStoreConnect.Models.Requests.UserInvitations;
using AppStoreConnect.Models.Responses;
using AppStoreConnect.Models.Responses.Common;
using AppStoreConnect.Models.Responses.UserInvitations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AppStoreConnect.Endpoints.UserInvitations
{
    public partial class UserInvitations : EndpointBase, IUserInvitations
    {
        public async Task<ApplicationResponse> InviteUser(UserInvitationCreateRequest payload)
        {
            var endpointUrl = $"{EndpointConstants.AppStoreConnectBaseUrl}/{EndpointConstants.Enpoints.UserInvitations}";
            var body = new StringContent(JsonSerializer.Serialize(payload, new JsonSerializerOptions { IgnoreNullValues = true }));
            body.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(EndpointConstants.JsonContentType);
            var result = await Client.PostAsync(endpointUrl, body);
            return result.ToPatternType() switch
            {
                { StatusCode: 201 } br => JsonSerializer.Deserialize<UserInvitationResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 400 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 403 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 409 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 422 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: _ } res => new InternalErrorResponse { Exception = new Exception(res.StatusCode.ToString()) },
            };
        }
    }
}
