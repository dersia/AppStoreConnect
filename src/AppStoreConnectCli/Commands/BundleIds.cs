﻿using AppStoreConnect;
using AppStoreConnect.Jwt;
using AppStoreConnect.Models.Enums;
using AppStoreConnect.Models.Responses.BundleIdCapabilities;
using AppStoreConnect.Models.Responses.BundleIds;
using AppStoreConnect.Models.Responses.Common;
using AppStoreConnect.Models.Responses.Profiles;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Linq;
using System.Text;
using AppStoreConnectCli.Common;
using AppStoreConnect.Models.Pocos.BundleIds;
using AppStoreConnect.Models.Pocos.BundleIdCapabilities;
using AppStoreConnect.Models.Pocos.Common;
using AppStoreConnect.Models.Pocos.Profiles;
using System.Threading.Tasks;
using System.Text.Json;
using AppStoreConnect.Models.Requests.BundleIds;
using System.IO;

namespace AppStoreConnectCli.Commands
{
    public static class BundleIds
    {
        public static Command CreateBundleIds()
        {
            var list = new Command("list");
            list.AddAlias("l");
            list.AddSubCommandArgument();
            list.Handler = CommandHandler.Create(typeof(BundleIds).GetMethod(nameof(List)));

            var get = new Command("get");
            get.AddAlias("g");
            get.AddArgument(new Argument<string>("bundleIdId"));
            get.AddSubCommandArgument();
            get.Handler = CommandHandler.Create(typeof(BundleIds).GetMethod(nameof(Get)));

            var register = new Command("register");
            register.AddAlias("r");
            register.AddArgument(new Argument<string>("identifier"));
            register.AddArgument(new Argument<string>("name"));
            register.AddArgument(new Argument<BundleIdPlatform>("platform"));
            register.AddSubCommandArgument();
            register.Handler = CommandHandler.Create(typeof(BundleIds).GetMethod(nameof(Register)));

            var registerFromJson = new Command("registerFromJson", "register a new bundleId from bundleId json");
            registerFromJson.AddAlias("rjson");
            registerFromJson.AddAlias("rj");
            registerFromJson.AddSubCommandArgument();
            registerFromJson.AddArgument(new Argument<string>("json"));
            registerFromJson.Handler = CommandHandler.Create(typeof(BundleIds).GetMethod(nameof(RegisterFromJson)));

            var registerFromFile = new Command("registerFromFile", "register a new bundleId from bundleId json file");
            registerFromFile.AddAlias("rfile");
            registerFromFile.AddAlias("rf");
            registerFromFile.AddSubCommandArgument();
            registerFromFile.AddArgument(new Argument<FileInfo>("file"));
            registerFromFile.Handler = CommandHandler.Create(typeof(BundleIds).GetMethod(nameof(RegisterFromFile)));

            var update = new Command("update");
            update.AddAlias("u");
            update.AddArgument(new Argument<string>("id"));
            update.AddOption(new Option<string?>("--name") { Argument = new Argument<string?>() { Arity = ArgumentArity.ZeroOrOne } });
            update.AddSubCommandArgument();
            update.Handler = CommandHandler.Create(typeof(BundleIds).GetMethod(nameof(Update)));

            var updateFromJson = new Command("updateFromJson", "update a bundleId from bundleId json");
            updateFromJson.AddAlias("ujson");
            updateFromJson.AddAlias("uj");
            updateFromJson.AddSubCommandArgument();
            updateFromJson.AddArgument(new Argument<string>("bundleIdId"));
            updateFromJson.AddArgument(new Argument<string>("json"));
            updateFromJson.Handler = CommandHandler.Create(typeof(BundleIds).GetMethod(nameof(UpdateFromJson)));

            var updateFromFile = new Command("updateFromFile", "update a bundleId from bundleId json file");
            updateFromFile.AddAlias("ufile");
            updateFromFile.AddAlias("uf");
            updateFromFile.AddSubCommandArgument();
            updateFromFile.AddArgument(new Argument<string>("bundleIdId"));
            updateFromFile.AddArgument(new Argument<FileInfo>("file"));
            updateFromFile.Handler = CommandHandler.Create(typeof(BundleIds).GetMethod(nameof(UpdateFromFile)));

            var delete = new Command("delete");
            delete.AddAlias("d");
            delete.AddArgument(new Argument<string>("bundleIdId"));
            delete.AddSubCommandArgument();
            delete.Handler = CommandHandler.Create(typeof(BundleIds).GetMethod(nameof(Delete)));

            var listLinkedBundleIdCapabilities = new Command("list-linkedBundleIdCapabilities", "list all capabilities for given bundleId");
            listLinkedBundleIdCapabilities.AddAlias("capabilities");
            listLinkedBundleIdCapabilities.AddArgument(new Argument<string>("bundleIdId"));
            listLinkedBundleIdCapabilities.AddSubCommandArgument();
            listLinkedBundleIdCapabilities.Handler = CommandHandler.Create(typeof(BundleIds).GetMethod(nameof(Capabilities)));

            var listLinkedBundleIdCapabilityIds = new Command("list-linkedBundleIdCapabilityIds", "list all capabilityIds for given bundleId");
            listLinkedBundleIdCapabilityIds.AddAlias("capabilityIds");
            listLinkedBundleIdCapabilityIds.AddArgument(new Argument<string>("bundleIdId"));
            listLinkedBundleIdCapabilityIds.AddSubCommandArgument();
            listLinkedBundleIdCapabilityIds.Handler = CommandHandler.Create(typeof(BundleIds).GetMethod(nameof(CapabilityIds)));

            var listLinkedProfileIds = new Command("list-linkedProfiles", "lists all profiles for given bundleId (no-content)");
            listLinkedProfileIds.AddAlias("profiles");
            listLinkedProfileIds.AddArgument(new Argument<string>("bundleIdId"));
            listLinkedProfileIds.AddSubCommandArgument();
            listLinkedProfileIds.Handler = CommandHandler.Create(typeof(BundleIds).GetMethod(nameof(Profiles)));

            var listLinkedProfiles = new Command("list-linkedProfileIds", "list all profileIds for given bundleId");
            listLinkedProfiles.AddAlias("profileIds");
            listLinkedProfiles.AddArgument(new Argument<string>("bundleIdId"));
            listLinkedProfiles.AddSubCommandArgument();
            listLinkedProfiles.Handler = CommandHandler.Create(typeof(BundleIds).GetMethod(nameof(ProfileIds)));

            return new Command("bundleIds", "create, update, list BundleIds")
            {
                list,
                get,
                register,
                registerFromJson,
                registerFromFile,
                update,
                delete,
                listLinkedBundleIdCapabilities,
                listLinkedBundleIdCapabilityIds,
                listLinkedProfileIds,
                listLinkedProfiles
            };
        }

        public static async Task ProfileIds(string token, string bundleIdId)
        {
            var result = await token.GetClient().BundleIds.GetLinkedProfileIds(bundleIdId);
            result.Handle<BundleIdProfilesLinkagesResponse>(res =>
            {
                if (res.LinkedProfileIds is null || !res.LinkedProfileIds.Any())
                {
                    Console.WriteLine("No Profiles");
                }
                else
                {
                    var count = 0;
                    foreach (var profile in res.LinkedProfileIds)
                    {
                        Console.WriteLine($"------------ Profile {++count} ----------");
                        profile?.Print();
                    }
                }
            });
        }

        public static async Task Profiles(string token, string bundleIdId)
        {
            var result = await token.GetClient().BundleIds.GetLinkedProfiles(bundleIdId);
            result.Handle<ProfilesResponse>(res =>
            {
                if (res.Profiles is null || !res.Profiles.Any())
                {
                    Console.WriteLine("No Profiles");
                }
                else
                {
                    var count = 0;
                    foreach (var profile in res.Profiles)
                    {
                        Console.WriteLine($"------------ Profile {++count} ----------");
                        profile?.Print(false);
                    }
                }
            });
        }

        public static async Task CapabilityIds(string token, string bundleIdId)
        {
            var result = await token.GetClient().BundleIds.GetLinkedBundleIdCapabilityIds(bundleIdId);
            result.Handle<BundleIdBundleIdCapabilitiesLinkagesResponse>(res =>
            {
                if (res.BundleIdCapabilityIds is null || !res.BundleIdCapabilityIds.Any())
                {
                    Console.WriteLine("No Capabilities");
                }
                else
                {
                    var count = 0;
                    foreach (var capability in res.BundleIdCapabilityIds)
                    {
                        Console.WriteLine($"------------ Capability {++count} ----------");
                        capability?.Print();
                    }
                }
            });
        }

        public static async Task Capabilities(string token, string bundleIdId)
        {
            var result = await token.GetClient().BundleIds.GetLinkedBundleIdCapabilities(bundleIdId);
            result.Handle<BundleIdCapabilitiesResponse>(res =>
            {
                if (res.BundleIdCapabilities is null || !res.BundleIdCapabilities.Any())
                {
                    Console.WriteLine("No Capabilities");
                }
                else
                {
                    var count = 0;
                    foreach (var capability in res.BundleIdCapabilities)
                    {
                        Console.WriteLine($"------------ Capability {++count} ----------");
                        capability?.Print();
                    }
                }
            });
        }

        public static async Task Delete(string token, string bundleIdId)
        {
            var result = await token.GetClient().BundleIds.DeleteBundleId(bundleIdId);
            result.Handle<NoContentResponse>(res =>
            {
                Console.WriteLine($"BundleId '{bundleIdId}' deleted");
            });
        }

        public static async Task Update(string token, string id, string? name)
        {
            var bundleId = new AppStoreConnect.Models.Pocos.BundleIds.BundleId();
            if (!string.IsNullOrWhiteSpace(name))
            {
                bundleId.Name = name;
            }
            var payload = new AppStoreConnect.Models.Requests.BundleIds.BundleIdUpdateRequest
            {
                BundleIdInformation = new AppStoreConnect.Models.Pocos.BundleIds.BundleIdInformation
                {
                    BundleId = bundleId,
                    Type = ResourceTypes.bundleIds,
                    Id = id
                }
            };
            var result = await token.GetClient().BundleIds.UpdateBundleId(id, payload);
            result.Handle<BundleIdResponse>(res =>
            {
                res.BundleIdInformation.Print();
            });
        }

        public static async Task Register(string token, string identifier, string name, BundleIdPlatform platform)
        {
            var payload = new AppStoreConnect.Models.Requests.BundleIds.BundleIdCreateRequest
            {
                BundleIdInformation = new AppStoreConnect.Models.Pocos.BundleIds.BundleIdInformation
                {
                    BundleId = new AppStoreConnect.Models.Pocos.BundleIds.BundleId
                    {
                        Identifier = identifier,
                        Name = name,
                        Platform = platform
                    },
                    Type = ResourceTypes.bundleIds
                }
            };
            Console.WriteLine(JsonSerializer.Serialize(payload, new JsonSerializerOptions { IgnoreNullValues = true }));
            var result = await token.GetClient().BundleIds.RegisterBundleId(payload);
            result.Handle<BundleIdResponse>(res =>
            {
                res.BundleIdInformation.Print();
            });
        }

        public static async Task Get(string bundleIdId, string token)
        {
            var result = await token.GetClient().BundleIds.GetBundleId(bundleIdId);
            result.Handle<BundleIdResponse>(res =>
            {
                res.BundleIdInformation?.Print();
            });
        }

        public static async Task List(string token)
        {
            var result = await token.GetClient().BundleIds.ListBundleIds();
            result.Handle<BundleIdsResponse>(res =>
            {
                var count = 0;
                if (res.BundleIdInformations is null || !res.BundleIdInformations.Any())
                {
                    Console.WriteLine("No BundleIds");
                }
                else
                {
                    foreach (var bundleId in res.BundleIdInformations)
                    {
                        Console.WriteLine($"------------ BundleId {++count} ----------");
                        bundleId.Print();
                    }
                }
            });
        }

        public static async Task RegisterFromFile(FileInfo file, string token)
        {
            var json = await file.OpenText().ReadToEndAsync();
            await RegisterFromJson(json, token);
        }

        public static async Task RegisterFromJson(string json, string token)
        {
            BundleIdCreateRequest? payload = null;
            try
            {
                payload = JsonSerializer.Deserialize<BundleIdCreateRequest>(json);
            }
            catch (Exception)
            {
            }
            AppStoreConnect.Models.Pocos.BundleIds.BundleId? bundleId = null;
            try
            {
                bundleId = JsonSerializer.Deserialize<AppStoreConnect.Models.Pocos.BundleIds.BundleId>(json);
            }
            catch (Exception)
            {
            }
            if (payload is null && bundleId is null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Could not deserialize json");
                return;
            }
            if (payload is null || payload.BundleIdInformation is null)
            {
                payload = new BundleIdCreateRequest()
                {
                    BundleIdInformation = new AppStoreConnect.Models.Pocos.BundleIds.BundleIdInformation
                    {
                        Type = ResourceTypes.bundleIds,
                        BundleId = bundleId
                    }
                };
            }
            var result = await token.GetClient().BundleIds.RegisterBundleId(payload);
            result.Handle<BundleIdResponse>(res =>
            {
                res?.BundleIdInformation?.Print();
            });
        }

        public static async Task UpdateFromFile(string bundleIdId, FileInfo file, string token)
        {
            var json = await file.OpenText().ReadToEndAsync();
            await UpdateFromJson(bundleIdId, json, token);
        }

        public static async Task UpdateFromJson(string bundleIdId, string json, string token)
        {
            BundleIdUpdateRequest? payload = null;
            try
            {
                payload = JsonSerializer.Deserialize<BundleIdUpdateRequest>(json);
            }
            catch (Exception)
            {
            }
            AppStoreConnect.Models.Pocos.BundleIds.BundleId? bundleId = null;
            try
            {
                bundleId = JsonSerializer.Deserialize<AppStoreConnect.Models.Pocos.BundleIds.BundleId>(json);
            }
            catch (Exception)
            {
            }
            if (payload is null && bundleId is null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Could not deserialize json");
                return;
            }
            if (payload is null || payload.BundleIdInformation is null)
            {
                payload = new BundleIdUpdateRequest()
                {
                    BundleIdInformation = new AppStoreConnect.Models.Pocos.BundleIds.BundleIdInformation
                    {
                        Type = ResourceTypes.bundleIds,
                        Id = bundleIdId,
                        BundleId = bundleId
                    }
                };
            }
            var result = await token.GetClient().BundleIds.UpdateBundleId(bundleIdId, payload);
            result.Handle<BundleIdResponse>(res =>
            {
                res?.BundleIdInformation?.Print();
            });
        }
    }
}
