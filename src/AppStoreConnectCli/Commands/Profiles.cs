using AppStoreConnect.Models.Enums;
using AppStoreConnect.Models.Pocos.Common;
using AppStoreConnect.Models.Requests.Profiles;
using AppStoreConnect.Models.Responses.BundleIds;
using AppStoreConnect.Models.Responses.Certificates;
using AppStoreConnect.Models.Responses.Common;
using AppStoreConnect.Models.Responses.Devices;
using AppStoreConnect.Models.Responses.Profiles;
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
    public static class Profiles
    {
        public static Command CreateProfiles()
        {
            var create = new Command("create", "create a new profile");
            create.AddAlias("c");
            create.AddArgument(new Argument<string>("name"));
            create.AddArgument(new Argument<ProfileType>("type"));
            create.AddOption(new Option<string[]>("--deviceId") { Argument = new Argument<string[]>() { Arity = ArgumentArity.OneOrMore } });
            create.AddOption(new Option<string[]>("--certificateId") { Argument = new Argument<string[]>() { Arity = ArgumentArity.OneOrMore } });
            create.AddOption(new Option<string>("--bundleIdId") { Argument = new Argument<string>() { Arity = ArgumentArity.ExactlyOne } });
            create.AddSubCommandArgument();
            create.Handler = CommandHandler.Create(
                async (string name, ProfileType type, string token, string[] deviceId, string[] certificateId, string bundleIdId) =>
                {
                    var profile = new AppStoreConnect.Models.Pocos.Profiles.Profile
                    {
                        Name = name,
                        ProfileType = type
                    };
                    var devices = new List<Data>();
                    var certificates = new List<Data>();
                    foreach(var device in deviceId)
                    {
                        devices.Add(new Data { Id = device, Type = ResourceTypes.devices });
                    }
                    foreach (var certificate in certificateId)
                    {
                        certificates.Add(new Data { Id = certificate, Type = ResourceTypes.certificates });
                    }
                    var payload = new ProfileCreateRequest()
                    {
                        ProfileInformation = new AppStoreConnect.Models.Pocos.Profiles.ProfileInformation
                        {
                            Type = ResourceTypes.profiles,
                            Profile = profile,
                            Relationships = new AppStoreConnect.Models.Pocos.Profiles.ProfileRelationships
                            {
                                BundleId = new Relationship { Id = new Data { Id = bundleIdId, Type = ResourceTypes.bundleIds } },
                                CertificateIds = new Relationships { Ids = certificates },
                                Devices = new Relationships { Ids = devices }
                            }
                        }
                    };
                    Console.WriteLine(JsonSerializer.Serialize(payload, new JsonSerializerOptions { IgnoreNullValues = true }));
                    var result = await token.GetClient().Profiles.CreateProfile(payload);
                    result.Handle<ProfileResponse>(res =>
                    {
                        res.ProfileInformation?.Print(false);
                    });
            });

            var createFromJson = new Command("createFromJson", "create a new profile from profile json");
            createFromJson.AddAlias("cjson");
            createFromJson.AddAlias("cj");
            createFromJson.AddSubCommandArgument();
            createFromJson.AddArgument(new Argument<string>("json"));
            createFromJson.Handler = CommandHandler.Create(async (string json, string token) => await CreateFromJson(json, token));

            var createFromFile = new Command("createFromFile", "create a new profile from profile json file");
            createFromFile.AddAlias("cfile");
            createFromFile.AddAlias("cf");
            createFromFile.AddSubCommandArgument();
            createFromFile.AddArgument(new Argument<FileInfo>("file"));
            createFromFile.Handler = CommandHandler.Create(async (FileInfo file, string token) => 
            {
                var json = await file.OpenText().ReadToEndAsync();
                await CreateFromJson(json, token);
            });

            var delete = new Command("delete", "delete a profile by its id");
            delete.AddAlias("d");
            delete.AddArgument(new Argument<string>("profileId"));
            delete.AddSubCommandArgument();
            delete.Handler = CommandHandler.Create(
                async (string profileId, string token) =>
                {
                    var result = await token.GetClient().Profiles.DeleteProfile(profileId);
                    result.Handle<NoContentResponse>(res =>
                    {
                        Console.WriteLine($"Profile '{profileId}' deleted");
                    });
                });

            var get = new Command("get", "get a profile by its id");
            get.AddAlias("g");
            get.AddArgument(new Argument<string>("profileId"));
            get.AddSubCommandArgument();
            get.Handler = CommandHandler.Create(async (string profileId, string token) => 
            {
                var result = await token.GetClient().Profiles.GetProfile(profileId);
                result.Handle<ProfileResponse>(res =>
                {
                    res?.ProfileInformation?.Print();
                });
            });

            var getContent = new Command("getContent", "get a profile content by its id");
            getContent.AddAlias("gc");
            getContent.AddArgument(new Argument<string>("profileId"));
            getContent.AddSubCommandArgument();
            getContent.Handler = CommandHandler.Create(async (string profileId, string token) =>
            {
                var result = await token.GetClient().Profiles.GetProfile(profileId);
                result.Handle<ProfileResponse>(res =>
                {
                    res?.ProfileInformation?.Print();
                });
            });

            var getNoContent = new Command("getEntry", "get a profile by its id (without content)");
            getNoContent.AddAlias("ge");
            getNoContent.AddArgument(new Argument<string>("profileId"));
            getNoContent.AddSubCommandArgument();
            getNoContent.Handler = CommandHandler.Create(async (string profileId, string token) =>
            {
                var result = await token.GetClient().Profiles.GetProfile(profileId);
                result.Handle<ProfileResponse>(res =>
                {
                    res?.ProfileInformation?.Print();
                });
            });

            var list = new Command("list", "list all profiles");
            list.AddAlias("l");
            list.AddSubCommandArgument();
            list.Handler = CommandHandler.Create(async (string token) =>
            {
                var result = await token.GetClient().Profiles.ListProfiles();
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
            });

            var linkedBundleId = new Command("linkedBundleId", "get bundleId linked to a profile");
            linkedBundleId.AddAlias("lbi");
            linkedBundleId.AddAlias("llbi");
            linkedBundleId.AddAlias("bundleId");
            linkedBundleId.AddArgument(new Argument<string>("profileId"));
            linkedBundleId.AddSubCommandArgument();
            linkedBundleId.Handler = CommandHandler.Create(async (string profileId, string token) =>
            {
                var result = await token.GetClient().Profiles.GetLinkedBundleId(profileId);
                result.Handle<BundleIdResponse>(res =>
                {
                    res.BundleIdInformation?.Print();
                });
            });

            var linkedBundleIdIds = new Command("linkedBundleIdId", "get bundleIdId linked to a profile");
            linkedBundleIdIds.AddAlias("lbiid");
            linkedBundleIdIds.AddAlias("llbiid");
            linkedBundleIdIds.AddAlias("bundleIdId");
            linkedBundleIdIds.AddArgument(new Argument<string>("profileId"));
            linkedBundleIdIds.AddSubCommandArgument();
            linkedBundleIdIds.Handler = CommandHandler.Create(async (string profileId, string token) =>
            {
                var result = await token.GetClient().Profiles.GetLinkedBundleIdId(profileId);
                result.Handle<ProfileBundleIdLinkageResponse>(res =>
                {
                    res.BundleIdId?.Print();
                });
            });

            var linkedCertificates = new Command("linkedCertificates", "list all certificates linked to a profile");
            linkedCertificates.AddAlias("lc");
            linkedCertificates.AddAlias("llc");
            linkedCertificates.AddAlias("certificates");
            linkedCertificates.AddArgument(new Argument<string>("profileId"));
            linkedCertificates.AddSubCommandArgument();
            linkedCertificates.Handler = CommandHandler.Create(async (string profileId, string token) =>
            {
                var result = await token.GetClient().Profiles.GetLinkedCertificates(profileId);
                result.Handle<CertificatesResponse>(res =>
                {
                    if (res.Certificates is null || !res.Certificates.Any())
                    {
                        Console.WriteLine("No Certificates");
                    }
                    else
                    {
                        var count = 0;
                        foreach (var certificate in res.Certificates)
                        {
                            Console.WriteLine($"------------ Certificate {++count} ----------");
                            certificate?.Print(false);
                        }
                    }
                });
            });

            var linkedCertificateIds = new Command("linkedCertificateIds", "list all certificateIds linked to a profile");
            linkedCertificateIds.AddAlias("lcid");
            linkedCertificateIds.AddAlias("llcid");
            linkedCertificateIds.AddAlias("certificateIds");
            linkedCertificateIds.AddArgument(new Argument<string>("profileId"));
            linkedCertificateIds.AddSubCommandArgument();
            linkedCertificateIds.Handler = CommandHandler.Create(async (string profileId, string token) =>
            {
                var result = await token.GetClient().Profiles.GetLinkedCertificateIds(profileId);
                result.Handle<ProfileCertificatesLinkagesResponse>(res =>
                {
                    if (res.Certificates is null || !res.Certificates.Any())
                    {
                        Console.WriteLine("No Certificates");
                    }
                    else
                    {
                        var count = 0;
                        foreach (var certificate in res.Certificates)
                        {
                            Console.WriteLine($"------------ Certificate {++count} ----------");
                            certificate?.Print();
                        }
                    }
                });
            });

            var linkedDevices = new Command("linkedDevices", "list all devices linked to a profile");
            linkedDevices.AddAlias("ld");
            linkedDevices.AddAlias("lld");
            linkedDevices.AddAlias("devices");
            linkedDevices.AddArgument(new Argument<string>("profileId"));
            linkedDevices.AddSubCommandArgument();
            linkedDevices.Handler = CommandHandler.Create(async (string profileId, string token) =>
            {
                var result = await token.GetClient().Profiles.GetLinkedDevices(profileId);
                result.Handle<DevicesResponse>(res =>
                {
                    if (res.Devices is null || !res.Devices.Any())
                    {
                        Console.WriteLine("No Devices");
                    }
                    else
                    {
                        var count = 0;
                        foreach (var device in res.Devices)
                        {
                            Console.WriteLine($"------------ Device {++count} ----------");
                            device?.Print();
                        }
                    }
                });
            });

            var linkedDeviceIds = new Command("linkedDeviceIds", "list all deviceIds linked to a profile");
            linkedDeviceIds.AddAlias("ldid");
            linkedDeviceIds.AddAlias("lldid");
            linkedDeviceIds.AddAlias("deviceIds");
            linkedDeviceIds.AddArgument(new Argument<string>("profileId"));
            linkedDeviceIds.AddSubCommandArgument();
            linkedDeviceIds.Handler = CommandHandler.Create(async (string profileId, string token) =>
            {
                var result = await token.GetClient().Profiles.GetLinkedDeviceIds(profileId);
                result.Handle<ProfileDevicesLinkagesResponse>(res =>
                {
                    if (res.Devices is null || !res.Devices.Any())
                    {
                        Console.WriteLine("No Devices");
                    }
                    else
                    {
                        var count = 0;
                        foreach (var device in res.Devices)
                        {
                            Console.WriteLine($"------------ Device {++count} ----------");
                            device?.Print();
                        }
                    }
                });
            });

            var profiles = new Command("profiles", "create, get, list or delete profiles")
            {
                create,
                createFromJson,
                createFromFile,
                get,
                getContent,
                getNoContent,
                list,
                delete,
                linkedBundleId,
                linkedBundleIdIds,
                linkedCertificates,
                linkedCertificateIds,
                linkedDevices,
                linkedDeviceIds
            };
            profiles.AddAlias("p");
            return profiles;
        }

        private static async Task CreateFromJson(string json, string token)
        {
            ProfileCreateRequest? payload = null;
            try
            {
                payload = JsonSerializer.Deserialize<ProfileCreateRequest>(json);
            }
            catch (Exception)
            {
            }
            if (payload is null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Could not deserialize json");
                return;
            }
            var result = await token.GetClient().Profiles.CreateProfile(payload);
            result.Handle<ProfileResponse>(res =>
            {
                res?.ProfileInformation?.Print(false);
            });
        }
    }
}
