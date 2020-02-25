using AppStoreConnect.Models.Filters.Apps;
using AppStoreConnect.Models.Filters.UserInvitations;
using AppStoreConnect.Models.Requests.UserInvitations;
using AppStoreConnect.Models.Responses.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppStoreConnect.Abstractions
{
    public interface IUserInvitations
    {
        Task<ApplicationResponse> CancelUserInvitation(string id);
        Task<ApplicationResponse> GetLinkedVisibleAppIds(string id, ListLinkedVisibleAppIdsFilters? filters = null);
        Task<ApplicationResponse> GetLinkedVisibleApps(string id, ListLinkedVisibleAppsFilters? filters = null);
        Task<ApplicationResponse> GetUserInvitation(string id, GetUserInvitationFilters? filters = null);
        Task<ApplicationResponse> InviteUser(UserInvitationCreateRequest payload);
        Task<ApplicationResponse> ListUserInvitations(ListUserInvitationsFilters? filters = null);
    }
}
