using AppStoreConnect.Models.Enums;
using AppStoreConnect.Models.Pocos.Devices;
using AppStoreConnect.Models.Requests.Certificates;
using AppStoreConnect.Models.Requests.Devices;
using AppStoreConnect.Models.Responses.Certificates;
using AppStoreConnect.Models.Responses.Common;
using AppStoreConnect.Models.Responses.Devices;
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
    public static class Devices
    {
        public static Command CreateDevices()
        {
            var register = new Command("register", "register a new device");
            register.AddAlias("r");
            register.AddArgument(new Argument<string>("name"));
            register.AddArgument(new Argument<DeviceClass>("class"));
            register.AddArgument(new Argument<BundleIdPlatform>("platform"));
            register.AddArgument(new Argument<string>("udid"));
            register.AddSubCommandArgument();
            register.AddOption(new Option<string>("--model"));
            register.Handler = CommandHandler.Create(
                async (string name, string? model, DeviceClass @class, BundleIdPlatform platform, string udid, string token) =>
                {
                    var device = new AppStoreConnect.Models.Pocos.Devices.Device
                    {
                        Name = name,
                        DeviceClass = @class,
                        Platform = platform,
                        Udid = udid
                    };
                    if(!string.IsNullOrWhiteSpace(model))
                    {
                        device.Model = model;
                    }
                    var payload = new DeviceCreateRequest()
                    {
                        DeviceInformation = new AppStoreConnect.Models.Pocos.Devices.DeviceInformation
                        {
                            Type = ResourceTypes.devices,
                            Device = device
                        }
                    };
                    var result = await token.GetClient().Devices.RegisterDevice(payload);
                    result.Handle<DeviceResponse>(res =>
                    {
                        res?.DeviceInformation?.Print();
                    });
            });

            var registerFromJson = new Command("registerFromJson", "register a new device from device json");
            registerFromJson.AddAlias("rjson");
            registerFromJson.AddAlias("rj");
            registerFromJson.AddSubCommandArgument();
            registerFromJson.AddArgument(new Argument<string>("json"));
            registerFromJson.Handler = CommandHandler.Create(async (string json, string token) => await RegisterFromJson(json, token));

            var registerFromFile = new Command("registerFromFile", "register a new device from device json file");
            registerFromFile.AddAlias("rfile");
            registerFromFile.AddAlias("rf");
            registerFromFile.AddSubCommandArgument();
            registerFromFile.AddArgument(new Argument<FileInfo>("file"));
            registerFromFile.Handler = CommandHandler.Create(async (FileInfo file, string token) => 
            {
                var json = await file.OpenText().ReadToEndAsync();
                await RegisterFromJson(json, token);
            });

            var update = new Command("update", "update a device");
            update.AddAlias("u");
            update.AddArgument(new Argument<string>("deviceId"));
            update.AddSubCommandArgument();
            update.AddOption(new Option<string?>("--name"));
            update.AddOption(new Option<DeviceClass>("--class"));
            update.AddOption(new Option<BundleIdPlatform>("--platform"));
            update.AddOption(new Option<string>("--udid"));
            update.AddOption(new Option<string?>("--model"));
            update.Handler = CommandHandler.Create(
                async (string deviceId, string? name, string? model, DeviceClass? @class, BundleIdPlatform? platform, string? udid, string token) =>
                {
                    var device = new AppStoreConnect.Models.Pocos.Devices.Device();
                    if (!string.IsNullOrWhiteSpace(model))
                    {
                        device.Model = model;
                    }
                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        device.Name = name;
                    }
                    if (!string.IsNullOrWhiteSpace(udid))
                    {
                        device.Udid = udid;
                    }
                    if (@class is { })
                    {
                        device.DeviceClass = @class;
                    }
                    if (platform is { })
                    {
                        device.Platform = platform;
                    }
                    var payload = new DeviceUpdateRequest()
                    {
                        DeviceInformation = new AppStoreConnect.Models.Pocos.Devices.DeviceInformation
                        {
                            Type = ResourceTypes.devices,
                            Device = device,
                            Id = deviceId
                        }
                    };
                    var result = await token.GetClient().Devices.UpdateDevice(deviceId, payload);
                    result.Handle<DeviceResponse>(res =>
                    {
                        res?.DeviceInformation?.Print();
                    });
                });

            var updateFromJson = new Command("updateFromJson", "update a device from device json");
            updateFromJson.AddAlias("ujson");
            updateFromJson.AddAlias("uj");
            updateFromJson.AddSubCommandArgument();
            updateFromJson.AddArgument(new Argument<string>("deviceId"));
            updateFromJson.AddArgument(new Argument<string>("json"));
            updateFromJson.Handler = CommandHandler.Create(async (string deviceId, string json, string token) => await UpdateFromJson(deviceId, json, token));

            var updateFromFile = new Command("updateFromFile", "update a device from device json file");
            updateFromFile.AddAlias("ufile");
            updateFromFile.AddAlias("uf");
            updateFromFile.AddSubCommandArgument();
            updateFromFile.AddArgument(new Argument<string>("deviceId"));
            updateFromFile.AddArgument(new Argument<FileInfo>("file"));
            updateFromFile.Handler = CommandHandler.Create(async (string deviceId, FileInfo file, string token) =>
            {
                var json = await file.OpenText().ReadToEndAsync();
                await UpdateFromJson(deviceId, json, token);
            });

            var get = new Command("get", "get a device by its id");
            get.AddAlias("g");
            get.AddArgument(new Argument<string>("deviceId"));
            get.AddSubCommandArgument();
            get.Handler = CommandHandler.Create(async (string deviceId, string token) => 
            {
                var result = await token.GetClient().Devices.GetDevice(deviceId);
                result.Handle<DeviceResponse>(res =>
                {
                    res?.DeviceInformation?.Print();
                    Console.WriteLine(JsonSerializer.Serialize(res, new JsonSerializerOptions { IgnoreNullValues = true }));
                });
            });

            var list = new Command("list", "list all devices");
            list.AddAlias("l");
            list.AddSubCommandArgument();
            list.Handler = CommandHandler.Create(async (string token) =>
            {
                var result = await token.GetClient().Devices.ListDevices();
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

            var devices = new Command("devices", "register, get, list or update devices")
            {
                register,
                registerFromJson,
                registerFromFile,
                update,
                updateFromJson,
                updateFromFile,
                get,
                list
            };
            devices.AddAlias("device");
            return devices;
        }

        private static async Task RegisterFromJson(string json, string token)
        {
            DeviceCreateRequest? payload = null;
            try
            {
                payload = JsonSerializer.Deserialize<DeviceCreateRequest>(json);
            }
            catch (Exception)
            {
            }
            AppStoreConnect.Models.Pocos.Devices.Device? device = null;
            try
            {
                device = JsonSerializer.Deserialize<AppStoreConnect.Models.Pocos.Devices.Device>(json);
            }
            catch (Exception)
            {
            }
            if (payload is null && device is null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Could not deserialize json");
                return;
            }
            if (payload is null || payload.DeviceInformation is null)
            {
                payload = new DeviceCreateRequest()
                {
                    DeviceInformation = new AppStoreConnect.Models.Pocos.Devices.DeviceInformation
                    {
                        Type = ResourceTypes.devices,
                        Device = device
                    }
                };
            }
            var result = await token.GetClient().Devices.RegisterDevice(payload);
            result.Handle<DeviceResponse>(res =>
            {
                res?.DeviceInformation?.Print();
            });
        }

        private static async Task UpdateFromJson(string id, string json, string token)
        {
            DeviceUpdateRequest? payload = null;
            try
            {
                payload = JsonSerializer.Deserialize<DeviceUpdateRequest>(json);
            }
            catch (Exception)
            {
            }
            AppStoreConnect.Models.Pocos.Devices.Device? device = null;
            try
            {
                device = JsonSerializer.Deserialize<AppStoreConnect.Models.Pocos.Devices.Device>(json);
            }
            catch (Exception)
            {
            }
            if (payload is null && device is null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Could not deserialize json");
                return;
            }
            if (payload is null || payload.DeviceInformation is null)
            {
                payload = new DeviceUpdateRequest()
                {
                    DeviceInformation = new AppStoreConnect.Models.Pocos.Devices.DeviceInformation
                    {
                        Type = ResourceTypes.devices,
                        Id = id,
                        Device = device
                    }
                };
            }
            var result = await token.GetClient().Devices.UpdateDevice(id, payload);
            result.Handle<DeviceResponse>(res =>
            {
                res?.DeviceInformation?.Print();
            });
        }
    }
}
