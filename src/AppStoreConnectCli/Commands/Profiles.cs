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
            create.AddOption(new Option<ProfileType>("--type") { Argument = new Argument<ProfileType> { Arity = ArgumentArity.ExactlyOne } });
            create.AddOption(new Option<string[]>("--deviceId") { Argument = new Argument<string[]>() { Arity = ArgumentArity.OneOrMore } });
            create.AddOption(new Option<string[]>("--certificateId") { Argument = new Argument<string[]>() { Arity = ArgumentArity.OneOrMore } });
            create.AddOption(new Option<string>("--bundleIdId") { Argument = new Argument<string>() { Arity = ArgumentArity.ExactlyOne } });
            create.AddSubCommandArgument();
            create.Handler = CommandHandler.Create(typeof(Profiles).GetMethod(nameof(Create)));

            var createFromJson = new Command("createFromJson", "create a new profile from profile json");
            createFromJson.AddAlias("cjson");
            createFromJson.AddAlias("cj");
            createFromJson.AddSubCommandArgument();
            createFromJson.AddArgument(new Argument<string>("json"));
            createFromJson.Handler = CommandHandler.Create(typeof(Profiles).GetMethod(nameof(CreateFromJson)));

            var createFromFile = new Command("createFromFile", "create a new profile from profile json file");
            createFromFile.AddAlias("cfile");
            createFromFile.AddAlias("cf");
            createFromFile.AddSubCommandArgument();
            createFromFile.AddArgument(new Argument<FileInfo>("file"));
            createFromFile.Handler = CommandHandler.Create(typeof(Profiles).GetMethod(nameof(CreateFromFile)));

            var delete = new Command("delete", "delete a profile by its id");
            delete.AddAlias("d");
            delete.AddArgument(new Argument<string>("profileId"));
            delete.AddSubCommandArgument();
            delete.Handler = CommandHandler.Create(typeof(Profiles).GetMethod(nameof(Delete)));

            var get = new Command("get", "get a profile by its id");
            get.AddAlias("g");
            get.AddArgument(new Argument<string>("profileId"));
            get.AddSubCommandArgument();
            get.Handler = CommandHandler.Create(typeof(Profiles).GetMethod(nameof(Get)));

            var getContent = new Command("getContent", "get a profile content by its id");
            getContent.AddAlias("gc");
            getContent.AddArgument(new Argument<string>("profileId"));
            getContent.AddSubCommandArgument();
            getContent.Handler = CommandHandler.Create(typeof(Profiles).GetMethod(nameof(GetContent)));

            var getNoContent = new Command("getEntry", "get a profile by its id (without content)");
            getNoContent.AddAlias("ge");
            getNoContent.AddArgument(new Argument<string>("profileId"));
            getNoContent.AddSubCommandArgument();
            getNoContent.Handler = CommandHandler.Create(typeof(Profiles).GetMethod(nameof(GetEntry)));

            var list = new Command("list", "list all profiles");
            list.AddAlias("l");
            list.AddSubCommandArgument();
            list.Handler = CommandHandler.Create(typeof(Profiles).GetMethod(nameof(List)));

            var linkedBundleId = new Command("linkedBundleId", "get bundleId linked to a profile");
            linkedBundleId.AddAlias("bundleId");
            linkedBundleId.AddArgument(new Argument<string>("profileId"));
            linkedBundleId.AddSubCommandArgument();
            linkedBundleId.Handler = CommandHandler.Create(typeof(Profiles).GetMethod(nameof(BundleId)));

            var linkedBundleIdIds = new Command("linkedBundleIdId", "get bundleIdId linked to a profile");
            linkedBundleIdIds.AddAlias("bundleIdId");
            linkedBundleIdIds.AddArgument(new Argument<string>("profileId"));
            linkedBundleIdIds.AddSubCommandArgument();
            linkedBundleIdIds.Handler = CommandHandler.Create(typeof(Profiles).GetMethod(nameof(BundleIdId)));

            var linkedCertificates = new Command("linkedCertificates", "list all certificates linked to a profile");
            linkedCertificates.AddAlias("certificates");
            linkedCertificates.AddArgument(new Argument<string>("profileId"));
            linkedCertificates.AddSubCommandArgument();
            linkedCertificates.Handler = CommandHandler.Create(typeof(Profiles).GetMethod(nameof(Certificates)));

            var linkedCertificateIds = new Command("linkedCertificateIds", "list all certificateIds linked to a profile");
            linkedCertificateIds.AddAlias("certificateIds");
            linkedCertificateIds.AddArgument(new Argument<string>("profileId"));
            linkedCertificateIds.AddSubCommandArgument();
            linkedCertificateIds.Handler = CommandHandler.Create(typeof(Profiles).GetMethod(nameof(CertificateIds)));

            var linkedDevices = new Command("linkedDevices", "list all devices linked to a profile");
            linkedDevices.AddAlias("devices");
            linkedDevices.AddArgument(new Argument<string>("profileId"));
            linkedDevices.AddSubCommandArgument();
            linkedDevices.Handler = CommandHandler.Create(typeof(Profiles).GetMethod(nameof(Devices)));

            var linkedDeviceIds = new Command("linkedDeviceIds", "list all deviceIds linked to a profile");
            linkedDeviceIds.AddAlias("deviceIds");
            linkedDeviceIds.AddArgument(new Argument<string>("profileId"));
            linkedDeviceIds.AddSubCommandArgument();
            linkedDeviceIds.Handler = CommandHandler.Create(typeof(Profiles).GetMethod(nameof(Devices)));

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

        public static async Task DeviceIds(string profileId, string token, bool outJson)
        {
            var result = await token.GetClient().Profiles.GetLinkedDeviceIds(profileId);
            result.Handle<ProfileDevicesLinkagesResponse>(res =>
            {
                res?.Print(outJson);
            }, outJson);
        }

        public static async Task Devices(string profileId, string token, bool outJson)
        {
            var result = await token.GetClient().Profiles.GetLinkedDevices(profileId);
            result.Handle<DevicesResponse>(res =>
            {
                res?.Print(outJson);
            }, outJson);
        }

        public static async Task CertificateIds(string profileId, string token, bool outJson)
        {
            var result = await token.GetClient().Profiles.GetLinkedCertificateIds(profileId);
            result.Handle<ProfileCertificatesLinkagesResponse>(res =>
            {
                res?.Print(outJson);
            }, outJson);
        }

        public static async Task Certificates(string profileId, string token, bool outJson)
        {
            var result = await token.GetClient().Profiles.GetLinkedCertificates(profileId);
            result.Handle<CertificatesResponse>(res =>
            {
                res?.Print(outJson, false);
            }, outJson);
        }

        public static async Task BundleIdId(string profileId, string token, bool outJson)
        {
            var result = await token.GetClient().Profiles.GetLinkedBundleIdId(profileId);
            result.Handle<ProfileBundleIdLinkageResponse>(res =>
            {
                res?.Print(outJson);
            }, outJson);
        }

        public static async Task BundleId(string profileId, string token, bool outJson)
        {
            var result = await token.GetClient().Profiles.GetLinkedBundleId(profileId);
            result.Handle<BundleIdResponse>(res =>
            {
                res?.Print(outJson);
            }, outJson);
        }

        public static async Task List(string token, bool outJson)
        {
            var result = await token.GetClient().Profiles.ListProfiles();
            result.Handle<ProfilesResponse>(res =>
            {
                res?.Print(outJson);
            }, outJson);
        }

        public static async Task GetEntry(string profileId, string token, bool outJson)
        {
            var result = await token.GetClient().Profiles.GetProfile(profileId);
            result.Handle<ProfileResponse>(res =>
            {
                res?.Print(outJson, false);
            }, outJson);
        }

        public static async Task GetContent(string profileId, string token, bool outJson)
        {
            var result = await token.GetClient().Profiles.GetProfile(profileId);
            result.Handle<ProfileResponse>(res =>
            {
                Console.WriteLine(res.ProfileInformation?.Profile?.ProfileContent);
            }, outJson);
        }

        public static async Task Get(string profileId, string token, bool outJson)
        {
            var result = await token.GetClient().Profiles.GetProfile(profileId);
            result.Handle<ProfileResponse>(res =>
            {
                res?.Print(outJson);
            }, outJson);
        }

        public static async Task Delete(string profileId, string token, bool outJson)
        {
            var result = await token.GetClient().Profiles.DeleteProfile(profileId);
            result.Handle<NoContentResponse>(res =>
            {
                res?.Print(() => { Console.WriteLine($"Profile '{profileId}' deleted"); }, outJson);
            }, outJson);
        }

        public static async Task Create(string name, ProfileType type, string token, string[] deviceId, string[] certificateId, string bundleIdId, bool outJson)
        {
            var profile = new AppStoreConnect.Models.Pocos.Profiles.Profile
            {
                Name = name,
                ProfileType = type
            };
            var devices = new List<Data>();
            var certificates = new List<Data>();
            foreach (var device in deviceId)
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
            var result = await token.GetClient().Profiles.CreateProfile(payload);
            result.Handle<ProfileResponse>(res =>
            {
                res?.Print(outJson, false);
            }, outJson);
        }

        public static async Task CreateFromFile(FileInfo file, string token, bool outJson)
        {
            var json = await file.OpenText().ReadToEndAsync();
            await CreateFromJson(json, token, outJson);
        }

        public static async Task CreateFromJson(string json, string token, bool outJson)
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
                res?.Print(outJson, false);
            }, outJson);
        }
    }
}
