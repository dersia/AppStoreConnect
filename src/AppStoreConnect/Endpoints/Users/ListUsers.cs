using AppStoreConnect.Abstractions;
using AppStoreConnect.Constants;
using AppStoreConnect.Endpoints.Base;
using AppStoreConnect.Extensions;
using AppStoreConnect.Models.Filters.Users;
using AppStoreConnect.Models.Requests;
using AppStoreConnect.Models.Responses;
using AppStoreConnect.Models.Responses.Common;
using AppStoreConnect.Models.Responses.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AppStoreConnect.Endpoints.Users
{
    public partial class Users : EndpointBase, IUsers
    {
        public async Task<ApplicationResponse> GetUsers(ListUsersFilters? filters = null)
        {
            var endpointUrl = $"{EndpointConstants.AppStoreConnectBaseUrl}/{EndpointConstants.Enpoints.Users}";
            if (filters is { })
            {
                endpointUrl += "?filler=1";
                if (filters.AppsProjectionFilter is { } apps && apps.Any())
                {
                    endpointUrl += $"{ListUsersFilters.Keys.FieldsApps}={string.Join(',', apps)}";
                }
                if (filters.UserProjectionFilter is { } users && users.Any())
                {
                    endpointUrl += $"{ListUsersFilters.Keys.FieldsUsers}={string.Join(',', users)}";
                }
                if (filters.IncludeVisibleAppsFilter is { } include)
                {
                    endpointUrl += $"{ListUsersFilters.Keys.Include}={ListUsersFilters.Include.VisibleApps}";
                }
                if (filters.LimitVisibleAppsFilter is { } limit)
                {
                    endpointUrl += $"{ListUsersFilters.Keys.LimitVisibleApps}={limit}";
                }
                if(filters.LimitUsersFilter is int usersLimit)
                {
                    endpointUrl += $"{ListUsersFilters.Keys.Limit}={usersLimit}";
                }
                if (filters.RolesFilter is { } roles && roles.Any())
                {
                    endpointUrl += $"{ListUsersFilters.Keys.FilterRoles}={string.Join(',', roles)}";
                }
                if (filters.SortBy is { } sort && sort.Any())
                {
                    endpointUrl += $"{ListUsersFilters.Keys.Sort}={string.Join(',', sort)}";
                }
                if (filters.UsersFilter is { } usersFilter && usersFilter.Any())
                {
                    endpointUrl += $"{ListUsersFilters.Keys.FilterUsername}={string.Join(',', usersFilter)}";
                }
                if (filters.VisibleAppsFilter is { } appsFilter && appsFilter.Any())
                {
                    endpointUrl += $"{ListUsersFilters.Keys.FilterVisibleApps}={string.Join(',', appsFilter)}";
                }
            }
            var result = await Client.GetAsync(endpointUrl);
            return result.ToPatternType() switch
            {
                { StatusCode: 200 } br => JsonSerializer.Deserialize<UsersResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 400 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 403 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: _ } res => await InternalErrorResponse.CreateFromResponseMessage(res.Response),
            };
        }
    }
}
