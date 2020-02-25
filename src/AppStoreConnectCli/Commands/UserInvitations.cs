using AppStoreConnect.Models.Enums;
using AppStoreConnect.Models.Requests.UserInvitations;
using AppStoreConnect.Models.Responses.Apps;
using AppStoreConnect.Models.Responses.Common;
using AppStoreConnect.Models.Responses.UserInvitations;
using AppStoreConnectCli.Common;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AppStoreConnectCli.Commands
{
    public static class UserInvitations
    {
        public static Command CreateUserInvitations()
        {
            var invite = new Command("invite", "invite a user");
            invite.AddAlias("i");
            invite.AddArgument(new Argument<string>("email"));
            invite.AddArgument(new Argument<string>("firstName"));
            invite.AddArgument(new Argument<string>("lastName"));
            invite.AddSubCommandArgument();
            invite.AddOption(new Option<UserRoles[]>("--roles") { Argument = new Argument<UserRoles[]> { Arity = ArgumentArity.ZeroOrMore } });
            invite.AddOption(new Option<bool?>("--allow-provivioning") { Argument = new Argument<bool?> { Arity = ArgumentArity.ZeroOrOne } });
            invite.AddOption(new Option<bool?>("--can-see-all-apps") { Argument = new Argument<bool?> { Arity = ArgumentArity.ZeroOrOne } });
            invite.Handler = CommandHandler.Create(
                async (string email, string firstName, string lastName, UserRoles[]? roles, bool? allowProvivioning, bool? canSeeAllApps, string token) =>
                {
                    var userInvitation = new AppStoreConnect.Models.Pocos.UserInvitations.UserInvitation
                    {
                        Email = email,
                        FirstName = firstName,
                        LastName = lastName,
                        Roles = roles is { } ? new List<UserRoles>(roles) : null,
                        ProvisioningAllowed = allowProvivioning,
                        AllAppsVisible = canSeeAllApps
                    };
                    var payload = new UserInvitationCreateRequest()
                    {
                        UserInvitationInformation = new AppStoreConnect.Models.Pocos.UserInvitations.UserInvitationInformation
                        {
                            Type = ResourceTypes.userInvitations,
                            UserInvitation = userInvitation
                        }
                    };
                    Console.WriteLine(JsonSerializer.Serialize(payload, new JsonSerializerOptions { IgnoreNullValues = true }));
                    var result = await token.GetClient().UserInvitations.InviteUser(payload);
                    result.Handle<UserInvitationResponse>(res =>
                    {
                        res.UserInvitationInformation?.Print();
                    });
            });

            var inviteFromJson = new Command("inviteFromJson", "invite a new user from userInvitation json");
            inviteFromJson.AddAlias("ijson");
            inviteFromJson.AddAlias("ij");
            inviteFromJson.AddSubCommandArgument();
            inviteFromJson.AddArgument(new Argument<string>("json"));
            inviteFromJson.Handler = CommandHandler.Create(async (string json, string token) => await InviteFromJson(json, token));

            var inviteFromFile = new Command("inviteFromFile", "invite a new user from userInvitation json file");
            inviteFromFile.AddAlias("ifile");
            inviteFromFile.AddAlias("if");
            inviteFromFile.AddSubCommandArgument();
            inviteFromFile.AddArgument(new Argument<FileInfo>("file"));
            inviteFromFile.Handler = CommandHandler.Create(async (FileInfo file, string token) => 
            {
                var json = await file.OpenText().ReadToEndAsync();
                await InviteFromJson(json, token);
            });

            var get = new Command("get", "get a user invitation by its id");
            get.AddAlias("g");
            get.AddArgument(new Argument<string>("userInvitationId"));
            get.AddSubCommandArgument();
            get.Handler = CommandHandler.Create(async (string userInvitationId, string token) => 
            {
                var result = await token.GetClient().UserInvitations.GetUserInvitation(userInvitationId);
                result.Handle<UserInvitationResponse>(res =>
                {
                    res.UserInvitationInformation?.Print();
                });
            });

            var list = new Command("list", "list all userInvitations");
            list.AddAlias("l");
            list.AddSubCommandArgument();
            list.Handler = CommandHandler.Create(async (string token) =>
            {
                var result = await token.GetClient().UserInvitations.ListUserInvitations();
                result.Handle<UserInvitationsResponse>(res =>
                {
                    if (res.UserInvitationInformations is null || !res.UserInvitationInformations.Any())
                    {
                        Console.WriteLine("No UserInvitations");
                    }
                    else
                    {
                        var count = 0;
                        foreach (var userInvitation in res.UserInvitationInformations)
                        {
                            Console.WriteLine($"------------ UserInvitations {++count} ----------");
                            userInvitation?.Print();
                        }
                    }
                });
            });

            var cancel = new Command("cancel", "cancel a userInvitation by its id");
            cancel.AddAlias("c");
            cancel.AddArgument(new Argument<string>("userInvitationId"));
            cancel.AddSubCommandArgument();
            cancel.Handler = CommandHandler.Create(async (string userInvitationId, string token) =>
            {
                var result = await token.GetClient().UserInvitations.CancelUserInvitation(userInvitationId);
                result.Handle<NoContentResponse>(res =>
                {
                    Console.WriteLine($"UserInvitation '{userInvitationId}' canceled");
                });
            });

            var linkedApps = new Command("linkedApps", "list all apps linked to a userInvitation by its id");
            linkedApps.AddAlias("la");
            linkedApps.AddAlias("lla");
            linkedApps.AddAlias("apps");
            linkedApps.AddArgument(new Argument<string>("userInvitationId"));
            linkedApps.AddSubCommandArgument();
            linkedApps.Handler = CommandHandler.Create(async (string userInvitationId, string token) =>
            {
                var result = await token.GetClient().UserInvitations.GetLinkedVisibleApps(userInvitationId);
                result.Handle<AppsResponse>(res =>
                {
                    if (res.AppInformations is null || !res.AppInformations.Any())
                    {
                        Console.WriteLine("No Apps");
                    }
                    else
                    {
                        var count = 0;
                        foreach (var app in res.AppInformations)
                        {
                            Console.WriteLine($"------------ Apps {++count} ----------");
                            app?.Print();
                        }
                    }
                });
            });

            var linkedAppIds = new Command("linkedAppIds", "list all appIds linked to a userInvitation by its id");
            linkedAppIds.AddAlias("laid");
            linkedAppIds.AddAlias("llaid");
            linkedAppIds.AddAlias("appIds");
            linkedAppIds.AddArgument(new Argument<string>("userInvitationId"));
            linkedAppIds.AddSubCommandArgument();
            linkedAppIds.Handler = CommandHandler.Create(async (string userInvitationId, string token) =>
            {
                var result = await token.GetClient().UserInvitations.GetLinkedVisibleAppIds(userInvitationId);
                result.Handle<UserInvitationVisibleAppsLinkagesResponse>(res =>
                {
                    if (res.LinkedVisibleApps is null || !res.LinkedVisibleApps.Any())
                    {
                        Console.WriteLine("No Apps");
                    }
                    else
                    {
                        var count = 0;
                        foreach (var app in res.LinkedVisibleApps)
                        {
                            Console.WriteLine($"------------ Apps {++count} ----------");
                            app?.Print();
                        }
                    }
                });
            });

            var userInvitations = new Command("userInvitations", "invite, get, list or cancel user invitations")
            {
                invite,
                inviteFromJson,
                inviteFromFile,
                get,
                list,
                cancel,
                linkedApps,
                linkedAppIds
            };
            userInvitations.AddAlias("userInvitation");
            return userInvitations;
        }

        private static async Task InviteFromJson(string json, string token)
        {
            UserInvitationCreateRequest? payload = null;
            try
            {
                payload = JsonSerializer.Deserialize<UserInvitationCreateRequest>(json);
            }
            catch (Exception)
            {
            }
            AppStoreConnect.Models.Pocos.UserInvitations.UserInvitation? userInvitation = null;
            try
            {
                userInvitation = JsonSerializer.Deserialize<AppStoreConnect.Models.Pocos.UserInvitations.UserInvitation>(json);
            }
            catch (Exception)
            {
            }
            if (payload is null && userInvitation is null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Could not deserialize json");
                return;
            }
            if (payload is null || payload.UserInvitationInformation is null)
            {
                payload = new UserInvitationCreateRequest()
                {
                    UserInvitationInformation = new AppStoreConnect.Models.Pocos.UserInvitations.UserInvitationInformation
                    {
                        Type = ResourceTypes.userInvitations,
                        UserInvitation = userInvitation
                    }
                };
            }
            var result = await token.GetClient().UserInvitations.InviteUser(payload);
            result.Handle<UserInvitationResponse>(res =>
            {
                res?.UserInvitationInformation?.Print();
            });
        }
    }
}
