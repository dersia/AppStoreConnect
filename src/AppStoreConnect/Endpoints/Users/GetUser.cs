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
        public async Task<ApplicationResponse> GetUser(string id, GetUserFilters? filters = null)
        {
            var endpointUrl = $"{EndpointConstants.AppStoreConnectBaseUrl}/{EndpointConstants.Enpoints.Users}/{id}";
            if (filters is { })
            {
                endpointUrl += "?filler=1";
                if(filters.AppsProjectionFilter is { } apps && apps.Any())
                {
                    endpointUrl += $"{GetUserFilters.Keys.FieldsApps}={string.Join(',', apps)}";
                }
                if (filters.UserProjectionFilter is { } users && users.Any())
                {
                    endpointUrl += $"{GetUserFilters.Keys.FieldsUsers}={string.Join(',', users)}";
                }
                if (filters.IncludeVisibleAppsFilter is { } include)
                {
                    endpointUrl += $"{GetUserFilters.Keys.Include}={GetUserFilters.Include.VisibleApps}";
                }
                if (filters.LimitVisibleAppsFilter is { } limit)
                {
                    endpointUrl += $"{GetUserFilters.Keys.LimitVisibleApps}={limit}";
                }
            }
            var result = await Client.GetAsync(endpointUrl);
            return result.ToPatternType() switch
            {
                { StatusCode: 200 } br => JsonSerializer.Deserialize<UserResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 400 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 403 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 404 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: _ } res => new InternalErrorResponse { Exception = new Exception(res.StatusCode.ToString()) },
            };
        }
    }
}
