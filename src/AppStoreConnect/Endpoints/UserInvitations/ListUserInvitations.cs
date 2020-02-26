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
        public async Task<ApplicationResponse> ListUserInvitations(ListUserInvitationsFilters? filters = null)
        {
            var endpointUrl = $"{EndpointConstants.AppStoreConnectBaseUrl}/{EndpointConstants.Enpoints.UserInvitations}";
            if (filters is { })
            {
                endpointUrl += "?filler=1";
                if (filters.AppsProjectionFilter is { } apps && apps.Any())
                {
                    endpointUrl += $"{ListUserInvitationsFilters.Keys.FieldsApps}={string.Join(',', apps)}";
                }
                if (filters.UserInvitationsProjectionFilter is { } invitations && invitations.Any())
                {
                    endpointUrl += $"{ListUserInvitationsFilters.Keys.FieldsUsers}={string.Join(',', invitations)}";
                }
                if (filters.IncludeVisibleAppsFilter is { } include)
                {
                    endpointUrl += $"{ListUserInvitationsFilters.Keys.Include}={ListUserInvitationsFilters.Include.VisibleApps}";
                }
                if (filters.LimitVisibleAppsFilter is { } limit)
                {
                    endpointUrl += $"{ListUserInvitationsFilters.Keys.LimitVisibleApps}={limit}";
                }
                if (filters.LimitUsersFilter is int usersLimit)
                {
                    endpointUrl += $"{ListUserInvitationsFilters.Keys.Limit}={usersLimit}";
                }
                if (filters.RolesFilter is { } roles && roles.Any())
                {
                    endpointUrl += $"{ListUserInvitationsFilters.Keys.FilterRoles}={string.Join(',', roles)}";
                }
                if (filters.SortBy is { } sort && sort.Any())
                {
                    endpointUrl += $"{ListUserInvitationsFilters.Keys.Sort}={string.Join(',', sort)}";
                }
                if (filters.EmailsFilter is { } emails && emails.Any())
                {
                    endpointUrl += $"{ListUserInvitationsFilters.Keys.FilterEmail}={string.Join(',', emails)}";
                }
                if (filters.VisibleAppsFilter is { } appsFilter && appsFilter.Any())
                {
                    endpointUrl += $"{ListUserInvitationsFilters.Keys.FilterVisibleApps}={string.Join(',', appsFilter)}";
                }
            }
            var result = await Client.GetAsync(endpointUrl);
            return result.ToPatternType() switch
            {
                { StatusCode: 200 } br => JsonSerializer.Deserialize<UserInvitationsResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 400 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 403 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: _ } res => await InternalErrorResponse.CreateFromResponseMessage(res.Response),
            };
        }
    }
}
