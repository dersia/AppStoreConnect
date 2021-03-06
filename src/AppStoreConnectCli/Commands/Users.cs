﻿using AppStoreConnect.Models.Enums;
using AppStoreConnect.Models.Pocos.Common;
using AppStoreConnect.Models.Requests.Users;
using AppStoreConnect.Models.Responses.Apps;
using AppStoreConnect.Models.Responses.Common;
using AppStoreConnect.Models.Responses.Users;
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
    public static class Users
    {
        public static Command CreateUsers()
        {
            var update = new Command("update", "update a user by their id");
            update.AddAlias("u");
            update.AddArgument(new Argument<string>("userId"));
            update.AddArgument(new Argument<string>("firstName"));
            update.AddArgument(new Argument<string>("lastName"));
            update.AddArgument(new Argument<UserRoles[]>("roles") { Arity = ArgumentArity.ZeroOrMore });
            update.AddSubCommandArgument();
            update.AddOption(new Option<bool?>("--allow-provivioning"));
            update.AddOption(new Option<bool?>("--can-see-all-apps"));
            update.Handler = CommandHandler.Create(typeof(Users).GetMethod(nameof(Update)));

            var updateFromJson = new Command("updateFromJson", "update user from user json");
            updateFromJson.AddAlias("ujson");
            updateFromJson.AddAlias("uj");
            updateFromJson.AddArgument(new Argument<string>("userId"));
            updateFromJson.AddSubCommandArgument();
            updateFromJson.AddArgument(new Argument<string>("json"));
            updateFromJson.Handler = CommandHandler.Create(typeof(Users).GetMethod(nameof(UpdateFromJson)));

            var updateFromFile = new Command("updateFromFile", "update user from user json file");
            updateFromFile.AddAlias("ufile");
            updateFromFile.AddAlias("uf");
            updateFromFile.AddArgument(new Argument<string>("userId"));
            updateFromFile.AddSubCommandArgument();
            updateFromFile.AddArgument(new Argument<FileInfo>("file"));
            updateFromFile.Handler = CommandHandler.Create(typeof(Users).GetMethod(nameof(UpdateFromFile)));

            var get = new Command("get", "get a user by their id");
            get.AddAlias("g");
            get.AddArgument(new Argument<string>("userId"));
            get.AddSubCommandArgument();
            get.Handler = CommandHandler.Create(typeof(Users).GetMethod(nameof(Get)));

            var list = new Command("list", "list all users");
            list.AddAlias("l");
            list.AddSubCommandArgument();
            list.Handler = CommandHandler.Create(typeof(Users).GetMethod(nameof(List)));

            var delete = new Command("delete", "delete a user by their id");
            delete.AddAlias("d");
            delete.AddArgument(new Argument<string>("userId"));
            delete.AddSubCommandArgument();
            delete.Handler = CommandHandler.Create(typeof(Users).GetMethod(nameof(Delete)));

            var linkedApps = new Command("linkedApps", "list all apps linked to a user by their id");
            linkedApps.AddAlias("apps");
            linkedApps.AddArgument(new Argument<string>("userId"));
            linkedApps.AddSubCommandArgument();
            linkedApps.Handler = CommandHandler.Create(typeof(Users).GetMethod(nameof(Apps)));

            var linkedAppIds = new Command("linkedAppIds", "list all appIds linked to a user by their id");
            linkedAppIds.AddAlias("appIds");
            linkedAppIds.AddArgument(new Argument<string>("userId"));
            linkedAppIds.AddSubCommandArgument();
            linkedAppIds.Handler = CommandHandler.Create(typeof(Users).GetMethod(nameof(AppIds)));

            var replaceApps = new Command("replaceApps", "replace apps for a user by their id");
            replaceApps.AddArgument(new Argument<string>("userId"));
            replaceApps.AddArgument(new Argument<Data[]>("apps") { Arity = ArgumentArity.OneOrMore });
            replaceApps.AddSubCommandArgument();
            replaceApps.Handler = CommandHandler.Create(typeof(Users).GetMethod(nameof(ReplaceApps)));

            var replaceAppsFromJson = new Command("replaceAppsFromJson", "replace apps for a user from List of Data json");
            replaceAppsFromJson.AddAlias("replaceAppsJson");
            replaceAppsFromJson.AddArgument(new Argument<string>("userId"));
            replaceAppsFromJson.AddSubCommandArgument();
            replaceAppsFromJson.AddArgument(new Argument<string>("json"));
            replaceAppsFromJson.Handler = CommandHandler.Create(typeof(Users).GetMethod(nameof(ReplaceAppsFromJson)));

            var replaceAppsFromFile = new Command("replaceAppsFromFile", "replace apps for a user from List of Data json file");
            replaceAppsFromFile.AddAlias("replaceAppsFile");
            replaceAppsFromFile.AddArgument(new Argument<string>("userId"));
            replaceAppsFromFile.AddSubCommandArgument();
            replaceAppsFromFile.AddArgument(new Argument<FileInfo>("file"));
            replaceAppsFromFile.Handler = CommandHandler.Create(typeof(Users).GetMethod(nameof(ReplaceAppsFromFile)));

            var removeApps = new Command("removeApps", "remove apps for a user by their id");
            removeApps.AddArgument(new Argument<string>("userId"));
            removeApps.AddArgument(new Argument<Data[]>("apps") { Arity = ArgumentArity.OneOrMore });
            removeApps.AddSubCommandArgument();
            removeApps.Handler = CommandHandler.Create(typeof(Users).GetMethod(nameof(RemoveApps)));

            var removeAppsFromJson = new Command("removeAppsFromJson", "remove apps for a user from List of Data json");
            removeAppsFromJson.AddAlias("removeAppsJson");
            removeAppsFromJson.AddArgument(new Argument<string>("userId"));
            removeAppsFromJson.AddSubCommandArgument();
            removeAppsFromJson.AddArgument(new Argument<string>("json"));
            removeAppsFromJson.Handler = CommandHandler.Create(typeof(Users).GetMethod(nameof(RemoveAppsFromJson)));

            var removeAppsFromFile = new Command("removeAppsFromFile", "remove apps for a user from List of Data json file");
            removeAppsFromFile.AddAlias("removeAppsFile");
            removeAppsFromFile.AddArgument(new Argument<string>("userId"));
            removeAppsFromFile.AddSubCommandArgument();
            removeAppsFromFile.AddArgument(new Argument<FileInfo>("file"));
            removeAppsFromFile.Handler = CommandHandler.Create(typeof(Users).GetMethod(nameof(RemoveAppsFromFile)));

            var addApps = new Command("addApps", "add apps for a user by their id");
            addApps.AddArgument(new Argument<string>("userId"));
            addApps.AddArgument(new Argument<Data[]>("apps") { Arity = ArgumentArity.OneOrMore });
            addApps.AddSubCommandArgument();
            addApps.Handler = CommandHandler.Create(typeof(Users).GetMethod(nameof(AddApps)));

            var addAppsFromJson = new Command("addAppsFromJson", "add apps for a user from List of Data json");
            addAppsFromJson.AddAlias("addAppsJson");
            addAppsFromJson.AddArgument(new Argument<string>("userId"));
            addAppsFromJson.AddSubCommandArgument();
            addAppsFromJson.AddArgument(new Argument<string>("json"));
            addAppsFromJson.Handler = CommandHandler.Create(typeof(Users).GetMethod(nameof(AddAppsFromJson)));

            var addAppsFromFile = new Command("addAppsFromFile", "add apps for a user from List of Data json file");
            addAppsFromFile.AddAlias("addAppsFile");
            addAppsFromFile.AddArgument(new Argument<string>("userId"));
            addAppsFromFile.AddSubCommandArgument();
            addAppsFromFile.AddArgument(new Argument<FileInfo>("file"));
            addAppsFromFile.Handler = CommandHandler.Create(typeof(Users).GetMethod(nameof(AddAppsFromFile)));

            var users = new Command("users", "update, get, list or delete a user | replace, remove or add apps to a user")
            {
                update,
                updateFromJson,
                updateFromFile,
                get,
                list,
                delete,
                linkedApps,
                linkedAppIds,
                replaceApps,
                replaceAppsFromJson,
                replaceAppsFromFile,
                removeApps,
                removeAppsFromJson,
                removeAppsFromFile,
                addApps,
                addAppsFromJson,
                addAppsFromFile
            };
            users.AddAlias("user");
            return users;
        }

        public static async Task AddApps(string userId, Data[] apps, string token, bool outJson)
        {
            var payload = new UserVisibleAppsLinkagesRequest()
            {
                Apps = apps.ToList()
            };
            var result = await token.GetClient().Users.AddVisibleAppsToUser(userId, payload);
            result.Handle<NoContentResponse>(res =>
            {
                res?.Print(() => Console.WriteLine($"Replaced '{apps.Length}' Apps for User '{userId}'"), outJson);
            }, outJson);
        }

        public static async Task RemoveApps(string userId, Data[] apps, string token, bool outJson)
        {
            var payload = new UserVisibleAppsLinkagesRequest()
            {
                Apps = apps.ToList()
            };
            var result = await token.GetClient().Users.RemoveVisibleAppsFromUser(userId, payload);
            result.Handle<NoContentResponse>(res =>
            {
                res?.Print(() => Console.WriteLine($"Replaced '{apps.Length}' Apps for User '{userId}'"), outJson);
            }, outJson);
        }

        public static async Task ReplaceApps(string userId, Data[] apps, string token, bool outJson)
        {
            var payload = new UserVisibleAppsLinkagesRequest()
            {
                Apps = apps.ToList()
            };
            var result = await token.GetClient().Users.ReplaceVisibleAppsForUser(userId, payload);
            result.Handle<NoContentResponse>(res =>
            {
                res?.Print(() => Console.WriteLine($"Replaced '{apps.Length}' Apps for User '{userId}'"), outJson);
            }, outJson);
        }

        public static async Task AppIds(string userId, string token, bool outJson)
        {
            var result = await token.GetClient().Users.GetLinkedVisibleAppIds(userId);
            result.Handle<UserVisibleAppsLinkagesResponse>(res =>
            {
                res?.Print(outJson);
            }, outJson);
        }

        public static async Task Apps(string userId, string token, bool outJson)
        {
            var result = await token.GetClient().Users.GetLinkedVisibleApps(userId);
            result.Handle<AppsResponse>(res =>
            {
                res?.Print(outJson);
            }, outJson);
        }

        public static async Task Delete(string userId, string token, bool outJson)
        {
            var result = await token.GetClient().Users.DeleteUser(userId);
            result.Handle<NoContentResponse>(res =>
            {
                res?.Print(() => Console.WriteLine($"User '{userId}' delete"), outJson);
            }, outJson);
        }

        public static async Task List(string token, bool outJson)
        {
            var result = await token.GetClient().Users.GetUsers();
            result.Handle<UsersResponse>(res =>
            {
                res?.Print(outJson);
            }, outJson);
        }

        public static async Task Get(string userId, string token, bool outJson)
        {
            var result = await token.GetClient().Users.GetUser(userId);
            result.Handle<UserResponse>(res => res?.Print(outJson), outJson);
        }

        public static async Task Update(string userId, string firstName, string lastName, UserRoles[] roles, bool? allowProvivioning, bool? canSeeAllApps, string token, bool outJson)
        {
            var user = new AppStoreConnect.Models.Pocos.Users.User
            {
                FirstName = firstName,
                LastName = lastName,
                UserRoles = new List<UserRoles>(roles),
                ProvisioningAllowed = allowProvivioning,
                AllAppsVisible = canSeeAllApps
            };
            if (allowProvivioning is bool)
            {
                user.ProvisioningAllowed = allowProvivioning;
            }
            if (canSeeAllApps is bool)
            {
                user.AllAppsVisible = canSeeAllApps;
            }
            var payload = new UserUpdateRequest()
            {
                UserInformation = new AppStoreConnect.Models.Pocos.Users.UserInformation
                {
                    Type = ResourceTypes.users,
                    User = user,
                    Id = userId
                }
            };
            var result = await token.GetClient().Users.UpdateUser(userId, payload);
            result.Handle<UserResponse>(res => res?.Print(outJson), outJson);
        }

        public static async Task UpdateFromFile(string userId, FileInfo file, string token, bool outJson)
        {
            var json = await file.OpenText().ReadToEndAsync();
            await UpdateFromJson(userId, json, token, outJson);
        }

        public static async Task UpdateFromJson(string userId, string json, string token, bool outJson)
        {
            UserUpdateRequest? payload = null;
            try
            {
                payload = JsonSerializer.Deserialize<UserUpdateRequest>(json);
            }
            catch (Exception)
            {
            }
            AppStoreConnect.Models.Pocos.Users.User? user = null;
            try
            {
                user = JsonSerializer.Deserialize<AppStoreConnect.Models.Pocos.Users.User>(json);
            }
            catch (Exception)
            {
            }
            if (payload is null && user is null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Could not deserialize json");
                return;
            }
            if (payload is null || payload.UserInformation is null)
            {
                payload = new UserUpdateRequest()
                {
                    UserInformation = new AppStoreConnect.Models.Pocos.Users.UserInformation
                    {
                        Type = ResourceTypes.users,
                        User = user,
                        Id = userId
                    }
                };
            }
            var result = await token.GetClient().Users.UpdateUser(userId, payload);
            result.Handle<UserResponse>(res =>
            {
                res?.Print(outJson);
            }, outJson);
        }

        public static async Task ReplaceAppsFromFile(string userId, FileInfo file, string token, bool outJson)
        {
            var json = await file.OpenText().ReadToEndAsync();
            await ReplaceAppsFromJson(userId, json, token, outJson);
        }

        public static async Task ReplaceAppsFromJson(string userId, string json, string token, bool outJson)
        {
            UserVisibleAppsLinkagesRequest? payload = null;
            try
            {
                payload = JsonSerializer.Deserialize<UserVisibleAppsLinkagesRequest>(json);
            }
            catch (Exception)
            {
            }
            List<AppStoreConnect.Models.Pocos.Common.Data>? apps = null;
            try
            {
                apps = JsonSerializer.Deserialize<List<AppStoreConnect.Models.Pocos.Common.Data>>(json);
            }
            catch (Exception)
            {
            }
            if (payload is null && apps is null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Could not deserialize json");
                return;
            }
            if (payload is null || payload.Apps is null)
            {
                payload = new UserVisibleAppsLinkagesRequest()
                {
                    Apps = apps
                };
            }
            var result = await token.GetClient().Users.ReplaceVisibleAppsForUser(userId, payload);
            result.Handle<NoContentResponse>(res =>
            {
                res?.Print(() => Console.WriteLine($"Replaced '{apps?.Count}' Apps for User '{userId}'"), outJson);
            }, outJson);
        }

        public static async Task RemoveAppsFromFile(string userId, FileInfo file, string token, bool outJson)
        {
            var json = await file.OpenText().ReadToEndAsync();
            await RemoveAppsFromJson(userId, json, token, outJson);
        }

        public static async Task RemoveAppsFromJson(string userId, string json, string token, bool outJson)
        {
            UserVisibleAppsLinkagesRequest? payload = null;
            try
            {
                payload = JsonSerializer.Deserialize<UserVisibleAppsLinkagesRequest>(json);
            }
            catch (Exception)
            {
            }
            List<AppStoreConnect.Models.Pocos.Common.Data>? apps = null;
            try
            {
                apps = JsonSerializer.Deserialize<List<AppStoreConnect.Models.Pocos.Common.Data>>(json);
            }
            catch (Exception)
            {
            }
            if (payload is null && apps is null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Could not deserialize json");
                return;
            }
            if (payload is null || payload.Apps is null)
            {
                payload = new UserVisibleAppsLinkagesRequest()
                {
                    Apps = apps
                };
            }
            var result = await token.GetClient().Users.RemoveVisibleAppsFromUser(userId, payload);
            result.Handle<NoContentResponse>(res =>
            {
                res?.Print(() => Console.WriteLine($"Replaced '{apps?.Count}' Apps for User '{userId}'"), outJson);
            }, outJson);
        }

        public static async Task AddAppsFromFile(string userId, FileInfo file, string token, bool outJson)
        {
            var json = await file.OpenText().ReadToEndAsync();
            await AddAppsFromJson(userId, json, token, outJson);
        }

        public static async Task AddAppsFromJson(string userId, string json, string token, bool outJson)
        {
            UserVisibleAppsLinkagesRequest? payload = null;
            try
            {
                payload = JsonSerializer.Deserialize<UserVisibleAppsLinkagesRequest>(json);
            }
            catch (Exception)
            {
            }
            List<AppStoreConnect.Models.Pocos.Common.Data>? apps = null;
            try
            {
                apps = JsonSerializer.Deserialize<List<AppStoreConnect.Models.Pocos.Common.Data>>(json);
            }
            catch (Exception)
            {
            }
            if (payload is null && apps is null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Could not deserialize json");
                return;
            }
            if (payload is null || payload.Apps is null)
            {
                payload = new UserVisibleAppsLinkagesRequest()
                {
                    Apps = apps
                };
            }
            var result = await token.GetClient().Users.AddVisibleAppsToUser(userId, payload);
            result.Handle<NoContentResponse>(res => 
            {
                res?.Print(() => Console.WriteLine($"Removed '{apps?.Count}' Apps for User '{userId}'"), outJson);
            }, outJson);
        }
    }
}
