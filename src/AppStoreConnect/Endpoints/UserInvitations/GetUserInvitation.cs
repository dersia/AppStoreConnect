using AppStoreConnect.Abstractions;
using AppStoreConnect.Constants;
using AppStoreConnect.Endpoints.Base;
using AppStoreConnect.Extensions;
using AppStoreConnect.Models.Filters.UserInvitations;
using AppStoreConnect.Models.Requests;
using AppStoreConnect.Models.Responses;
using AppStoreConnect.Models.Responses.Common;
using AppStoreConnect.Models.Responses.UserInvitations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AppStoreConnect.Endpoints.UserInvitations
{
    public partial class UserInvitations : EndpointBase, IUserInvitations
    {
        public async Task<ApplicationResponse> GetUserInvitation(string id, GetUserInvitationFilters? filters = null)
        {
            var endpointUrl = $"{EndpointConstants.AppStoreConnectBaseUrl}/{EndpointConstants.Enpoints.UserInvitations}/{id}";
            if (filters is { })
            {
                endpointUrl += "?filler=1";
                if (filters.AppsProjectionFilter is { } apps && apps.Any())
                {
                    endpointUrl += $"{GetUserInvitationFilters.Keys.FieldsApps}={string.Join(',', apps)}";
                }
                if (filters.UserInvitationsProjectionFilter is { } invitations && invitations.Any())
                {
                    endpointUrl += $"{GetUserInvitationFilters.Keys.FieldsUsers}={string.Join(',', invitations)}";
                }
                if (filters.IncludeVisibleAppsFilter is { } include)
                {
                    endpointUrl += $"{GetUserInvitationFilters.Keys.Include}={GetUserInvitationFilters.Include.VisibleApps}";
                }
                if (filters.LimitVisibleAppsFilter is { } limit)
                {
                    endpointUrl += $"{GetUserInvitationFilters.Keys.LimitVisibleApps}={limit}";
                }
            }
            var result = await Client.GetAsync(endpointUrl);
            return result.ToPatternType() switch
            {
                { StatusCode: 200 } br => JsonSerializer.Deserialize<UserInvitationResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 400 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 403 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 404 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: _ } res => new InternalErrorResponse { Exception = new Exception(res.StatusCode.ToString()) },
            };
        }
    }
}
