using AppStoreConnect.Abstractions;
using AppStoreConnect.Constants;
using AppStoreConnect.Endpoints.Base;
using AppStoreConnect.Extensions;
using AppStoreConnect.Models.Filters.Apps;
using AppStoreConnect.Models.Requests;
using AppStoreConnect.Models.Responses;
using AppStoreConnect.Models.Responses.Apps;
using AppStoreConnect.Models.Responses.Common;
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
        public async Task<ApplicationResponse> GetLinkedVisibleApps(string id, ListLinkedVisibleAppsFilters? filters = null)
        {
            var endpointUrl = $"{EndpointConstants.AppStoreConnectBaseUrl}/{EndpointConstants.Enpoints.Users}/{id}/{EndpointConstants.Enpoints.VisibleApps}";
            if (filters is { })
            {
                endpointUrl += "?filler=1";
                if(filters.AppsProjectionFilter is { } apps && apps.Any())
                {
                    endpointUrl += $"{ListLinkedVisibleAppsFilters.Keys.FieldsApps}={string.Join(',', apps)}";
                }
                if (filters.LimitVisibleAppsFilter is { } limit)
                {
                    endpointUrl += $"{ListLinkedVisibleAppsFilters.Keys.LimitVisibleApps}={limit}";
                }
            }
            var result = await Client.GetAsync(endpointUrl);
            return result.ToPatternType() switch
            {
                { StatusCode: 200 } br => JsonSerializer.Deserialize<AppsResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 400 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 403 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 404 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: _ } res => await InternalErrorResponse.CreateFromResponseMessage(res.Response),
            };
        }
    }
}
