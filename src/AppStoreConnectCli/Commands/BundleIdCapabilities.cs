using AppStoreConnect;
using AppStoreConnect.Jwt;
using AppStoreConnect.Models.Pocos.BundleIdCapabilities;
using AppStoreConnect.Models.Requests.BundleIdCapabilities;
using AppStoreConnect.Models.Responses.BundleIdCapabilities;
using AppStoreConnect.Models.Responses.Common;
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
    public static class BundleIdCapabilities
    {
        public static Command CreateBundleIdCapabilities()
        {
            var enableFromJson = new Command("enableFromJson", "enable capability from BundleIdCapability json");
            enableFromJson.AddAlias("ejson");
            enableFromJson.AddAlias("ej");
            enableFromJson.AddSubCommandArgument();
            enableFromJson.AddArgument(new Argument<string>("json"));
            enableFromJson.Handler = CommandHandler.Create(typeof(BundleIdCapabilities).GetMethod(nameof(EnableFromJson)));

            var enableFromFile = new Command("enableFromFile", "enable capability from BundleIdCapability json file");
            enableFromFile.AddAlias("efile");
            enableFromFile.AddAlias("ef");
            enableFromFile.AddSubCommandArgument();
            enableFromFile.AddArgument(new Argument<FileInfo>("file"));
            enableFromFile.Handler = CommandHandler.Create(typeof(BundleIdCapabilities).GetMethod(nameof(EnableFromFile)));

            var disable = new Command("disable");
            disable.AddAlias("d");
            disable.AddArgument(new Argument<string>("capabilityId"));
            disable.AddSubCommandArgument();
            disable.Handler = CommandHandler.Create(typeof(BundleIdCapabilities).GetMethod(nameof(Disable)));

            var modifyFromJson = new Command("modifyFromJson", "modify capability from BundleIdCapability json");
            modifyFromJson.AddAlias("mjson");
            modifyFromJson.AddAlias("mj");
            modifyFromJson.AddSubCommandArgument();
            modifyFromJson.AddArgument(new Argument<string>("capabilityId"));
            modifyFromJson.AddArgument(new Argument<string>("json"));
            modifyFromJson.Handler = CommandHandler.Create(typeof(BundleIdCapabilities).GetMethod(nameof(ModifyFromJson)));

            var modifyFromFile = new Command("modifyFromFile", "modify capability from BundleIdCapability json file");
            modifyFromFile.AddAlias("mfile");
            modifyFromFile.AddAlias("mf");
            modifyFromFile.AddSubCommandArgument();
            modifyFromFile.AddArgument(new Argument<string>("capabilityId"));
            modifyFromFile.AddArgument(new Argument<FileInfo>("file"));
            modifyFromFile.Handler = CommandHandler.Create(typeof(BundleIdCapabilities).GetMethod(nameof(ModifyFromFile)));

            return new Command("bundleIdCapability", "enable, disable and modify BundleId-Capabilities")
            {
                enableFromJson,
                enableFromFile,
                disable,
                //modifyFromJson,
                //modifyFromFile
            };
        }

        public static async Task Disable(string token, string capabilityId)
        {
            if (capabilityId is null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("capability id is needed!");
                return;
            }
            var result = await token.GetClient().BundleIdCapabilities.DisableCapability(capabilityId);
            result.Handle<NoContentResponse>(res =>
            {
                Console.WriteLine($"capability '{capabilityId}' disabled");
            });
        }

        public static async Task EnableFromFile(FileInfo file, string token)
        {
            var json = await file.OpenText().ReadToEndAsync();
            await EnableFromJson(json, token);
        }

        public static async Task EnableFromJson(string json, string token)
        {
            BundleIdCapabilityCreateRequest? payload = null;
            BundleIdCapability? capability = null;
            try
            {
                payload = JsonSerializer.Deserialize<BundleIdCapabilityCreateRequest>(json);
            }
            catch (Exception) { }
            try
            {
                capability = JsonSerializer.Deserialize<BundleIdCapability>(json);
            }
            catch (Exception) { }
            if(capability is null && payload is null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Could not deserialize json");
                return;
            }
            if(payload is null || payload.BundleIdCapabilityInformation is null)
            {
                payload = new BundleIdCapabilityCreateRequest
                {
                    BundleIdCapabilityInformation = new BundleIdCapabilityInformation
                    {
                        Type = AppStoreConnect.Models.Enums.ResourceTypes.bundleIdCapabilities,
                        BundleIdCapability = capability
                    }
                };
            }
            var result = await token.GetClient().BundleIdCapabilities.EnableCapability(payload);
            result.Handle<BundleIdCapabilityResponse>(res =>
            {
                res.BundleIdCapability?.Print();
            });
        }

        public static async Task ModifyFromFile(string capabilityId, FileInfo file, string token)
        {
            var json = await file.OpenText().ReadToEndAsync();
            await ModifyFromJson(capabilityId, json, token);
        }

        public static async Task ModifyFromJson(string capabilityId, string json, string token)
        {
            BundleIdCapabilityUpdateRequest? payload = null;
            BundleIdCapability? capability = null;
            try
            {
                payload = JsonSerializer.Deserialize<BundleIdCapabilityUpdateRequest>(json);
            }
            catch (Exception) { }
            try
            {
                capability = JsonSerializer.Deserialize<BundleIdCapability>(json);
            }
            catch (Exception) { }
            if (capability is null && payload is null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Could not deserialize json");
                return;
            }
            if (payload is null || payload.BundleIdCapabilityInformation is null)
            {
                payload = new BundleIdCapabilityUpdateRequest
                {
                    BundleIdCapabilityInformation = new BundleIdCapabilityInformation
                    {
                        Type = AppStoreConnect.Models.Enums.ResourceTypes.bundleIdCapabilities,
                        BundleIdCapability = capability
                    }
                };
            }
            var result = await token.GetClient().BundleIdCapabilities.ModifyCapability(capabilityId, payload);
            result.Handle<BundleIdCapabilityResponse>(res =>
            {
                res.BundleIdCapability?.Print();
            });
        }
    }
}
