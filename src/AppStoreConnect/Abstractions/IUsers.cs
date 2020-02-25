using AppStoreConnect.Models.Filters.Apps;
using AppStoreConnect.Models.Filters.Users;
using AppStoreConnect.Models.Requests.Users;
using AppStoreConnect.Models.Responses.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppStoreConnect.Abstractions
{
    public interface IUsers
    {
        Task<ApplicationResponse> AddVisibleAppsToUser(string id, UserVisibleAppsLinkagesRequest userVisibleAppsLinkagesRequest);
        Task<ApplicationResponse> DeleteUser(string id);
        Task<ApplicationResponse> GetLinkedVisibleAppIds(string id, ListLinkedVisibleAppIdsFilters? filters = null);
        Task<ApplicationResponse> GetLinkedVisibleApps(string id, ListLinkedVisibleAppsFilters? filters = null);
        Task<ApplicationResponse> GetUser(string id, GetUserFilters? filters = null);
        Task<ApplicationResponse> GetUsers(ListUsersFilters? filters = null);
        Task<ApplicationResponse> RemoveVisibleAppsFromUser(string id, UserVisibleAppsLinkagesRequest userVisibleAppsLinkagesRequest);
        Task<ApplicationResponse> ReplaceVisibleAppsForUser(string id, UserVisibleAppsLinkagesRequest userVisibleAppsLinkagesRequest);
        Task<ApplicationResponse> UpdateUser(string id, UserUpdateRequest payload);
    }
}
