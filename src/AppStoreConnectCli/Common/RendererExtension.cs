using AppStoreConnect.Models.Pocos.Apps;
using AppStoreConnect.Models.Pocos.BundleIdCapabilities;
using AppStoreConnect.Models.Pocos.BundleIds;
using AppStoreConnect.Models.Pocos.Certificates;
using AppStoreConnect.Models.Pocos.Common;
using AppStoreConnect.Models.Pocos.Devices;
using AppStoreConnect.Models.Pocos.Profiles;
using AppStoreConnect.Models.Pocos.UserInvitations;
using AppStoreConnect.Models.Pocos.Users;
using AppStoreConnect.Models.Responses.Apps;
using AppStoreConnect.Models.Responses.BundleIdCapabilities;
using AppStoreConnect.Models.Responses.BundleIds;
using AppStoreConnect.Models.Responses.Certificates;
using AppStoreConnect.Models.Responses.Common;
using AppStoreConnect.Models.Responses.Devices;
using AppStoreConnect.Models.Responses.Profiles;
using AppStoreConnect.Models.Responses.UserInvitations;
using AppStoreConnect.Models.Responses.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AppStoreConnectCli.Common
{
    public static class RendererExtension
    {
        public static async Task Print(this NoContentResponse response, Func<Task> execute, bool outJson)
        {
            if(PrintJson(response, outJson))
            {
                return;
            }
            await execute();
        }

        public static void Print(this NoContentResponse response, Action execute, bool outJson)
        {
            if (PrintJson(response, outJson))
            {
                return;
            }
            execute();
        }

        public static void Print(this BundleIdsResponse? bundleIds, bool outJson)
        {
            if (PrintJson(bundleIds, outJson))
            {
                return;
            }
            if (bundleIds?.BundleIdInformations is null || !bundleIds.BundleIdInformations.Any())
            {
                Console.WriteLine("No BundleIds");
            }
            else
            {
                var count = 0;
                foreach (var bundleId in bundleIds.BundleIdInformations)
                {
                    Console.WriteLine($"------------ Profile {++count} ----------");
                    bundleId.Print();
                }
            }
        }

        public static void Print(this BundleIdResponse? bundleId, bool outJson)
        {
            if (PrintJson(bundleId, outJson))
            {
                return;
            }
            bundleId?.BundleIdInformation?.Print();
        }

        private static void Print(this BundleIdInformation? bundleId)
        {
            Console.WriteLine($"Name: {bundleId?.BundleId?.Name}");
            Console.WriteLine($"Identifier: {bundleId?.BundleId?.Identifier}");
            Console.WriteLine($"Platform: {bundleId?.BundleId?.Platform}");
            Console.WriteLine($"SeedId: {bundleId?.BundleId?.SeedId}");
            Console.WriteLine($"Id: {bundleId?.Id}");
        }

        public static void Print(this BundleIdCapabilityResponse? capability, bool outJson)
        {
            if (PrintJson(capability, outJson))
            {
                return;
            }
            capability?.BundleIdCapability?.Print();
        }

        public static void Print(this BundleIdCapabilitiesResponse? capabilities, bool outJson)
        {
            if (PrintJson(capabilities, outJson))
            {
                return;
            }
            if (capabilities?.BundleIdCapabilities is null || !capabilities.BundleIdCapabilities.Any())
            {
                Console.WriteLine("No Capabilities");
            }
            else
            {
                var count = 0;
                foreach (var capability in capabilities.BundleIdCapabilities)
                {
                    Console.WriteLine($"------------ Capability {++count} ----------");
                    capability?.Print();
                }
            }
        }

        private static void Print(this BundleIdCapabilityInformation capability)
        {
            Console.WriteLine($"Id: {capability?.Id}");
            Console.WriteLine($"Type: {capability?.BundleIdCapability?.CapabilityType}");
            if (capability?.BundleIdCapability?.CapabilitySettings?.Any() ?? false)
            {
                Console.WriteLine($"Settings:");
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                foreach (var setting in capability?.BundleIdCapability?.CapabilitySettings)
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                {
                    Console.WriteLine($"  -Name: {setting?.Name}");
                    Console.WriteLine($"  -Description: {setting?.Description}");
                    Console.WriteLine($"  -AllowedInstances: {setting?.AllowedInstances}");
                    Console.WriteLine($"  -EnabledByDefault: {setting?.EnabledByDefault}");
                    Console.WriteLine($"  -Key: {setting?.Key}");
                    Console.WriteLine($"  -MinInstances: {setting?.MinInstances}");
                    Console.WriteLine($"  -Visible: {setting?.Visible}");
                    if (setting?.Options?.Any() ?? false)
                    {
                        Console.WriteLine($"  -Options:");
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                        foreach (var option in setting?.Options)
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                        {
                            Console.WriteLine($"    -Name: {option?.Name}");
                            Console.WriteLine($"    -Description: {option?.Description}");
                            Console.WriteLine($"    -Enabled: {option?.Enabled}");
                            Console.WriteLine($"    -EnabledByDefault: {option?.EnabledByDefault}");
                            Console.WriteLine($"    -Key: {option?.Key}");
                            Console.WriteLine($"    -SupportsWildcard: {option?.SupportsWildcard}");
                        }
                    }
                }
            }
        }

        public static void Print(this ProfilesResponse? profiles, bool outJson, bool withContent = true)
        {
            if (PrintJson(profiles, outJson))
            {
                return;
            }
            if (profiles?.Profiles is null || !profiles.Profiles.Any())
            {
                Console.WriteLine("No Profiles");
            }
            else
            {
                var count = 0;
                foreach (var profile in profiles.Profiles)
                {
                    Console.WriteLine($"------------ Profile {++count} ----------");
                    profile?.Print(withContent);
                }
            }
        }

        public static void Print(this ProfileResponse? profile, bool outJson, bool withContent = true)
        {
            if (PrintJson(profile, outJson))
            {
                return;
            }
            profile?.ProfileInformation?.Print(withContent);
        }

        private static void Print(this ProfileInformation? profile, bool withContent = true)
        {
            Console.WriteLine($"Name: {profile?.Profile?.Name}");
            Console.WriteLine($"CreatedDate: {profile?.Profile?.CreatedDate}");
            Console.WriteLine($"ExpirationDate: {profile?.Profile?.ExpirationDate}");
            Console.WriteLine($"Platform: {profile?.Profile?.Platform}");
            Console.WriteLine($"ProfileState: {profile?.Profile?.ProfileState}");
            Console.WriteLine($"ProfileType: {profile?.Profile?.ProfileType}");
            Console.WriteLine($"Uuid: {profile?.Profile?.Uuid}");
            Console.WriteLine($"Id: {profile?.Id}");
            if (withContent)
            {
                Console.WriteLine($"ProfileContent: {profile?.Profile?.ProfileContent}");
            }
        }

        public static void Print(this CertificatesResponse? certificates, bool outJson, bool withContent = true)
        {
            if (PrintJson(certificates, outJson))
            {
                return;
            }
            if (certificates?.Certificates is null || !certificates.Certificates.Any())
            {
                Console.WriteLine("No Certificates");
            }
            else
            {
                var count = 0;
                foreach (var certificate in certificates.Certificates)
                {
                    Console.WriteLine($"------------ Cert {++count} ----------");
                    certificate.Print(withContent);
                }
            }            
        }

        public static void Print(this CertificateResponse? certificate, bool outJson, bool withContent = true)
        {
            if (PrintJson(certificate, outJson))
            {
                return;
            }
            certificate?.CertificateInformation?.Print(withContent);
        }

        private static void Print(this CertificateInformation? certificate, bool withContent = true)
        {
            Console.WriteLine($"Name: {certificate?.Certificate?.Name}");
            Console.WriteLine($"DisplayName: {certificate?.Certificate?.DisplayName}");
            Console.WriteLine($"ExpirationDate: {certificate?.Certificate?.ExpirationDate}");
            Console.WriteLine($"Platform: {certificate?.Certificate?.Platform}");
            Console.WriteLine($"CertificateType: {certificate?.Certificate?.CertificateType}");
            Console.WriteLine($"SerialNumber: {certificate?.Certificate?.SerialNumber}");
            Console.WriteLine($"Id: {certificate?.Id}");
            if (withContent)
            {
                Console.WriteLine($"CertificateContent: {certificate?.Certificate?.CertificateContent}");
            }
        }

        public static void Print(this DevicesResponse? devices, bool outJson)
        {
            if (PrintJson(devices, outJson))
            {
                return;
            }
            if (devices?.Devices is null || !devices.Devices.Any())
            {
                Console.WriteLine("No Devices");
            }
            else
            {
                var count = 0;
                foreach (var device in devices.Devices)
                {
                    Console.WriteLine($"------------ Device {++count} ----------");
                    device?.Print();
                }
            }            
        }

        public static void Print(this DeviceResponse? device, bool outJson)
        {
            if (PrintJson(device, outJson))
            {
                return;
            }
            device?.DeviceInformation?.Print();
        }

        private static void Print(this DeviceInformation? device)
        {
            Console.WriteLine($"Name: {device?.Device?.Name}");
            Console.WriteLine($"Model: {device?.Device?.Model}");
            Console.WriteLine($"Udid: {device?.Device?.Udid}");
            Console.WriteLine($"Status: {device?.Device?.Status}");
            Console.WriteLine($"Platform: {device?.Device?.Platform}");
            Console.WriteLine($"DeviceClass: {device?.Device?.DeviceClass}");
            Console.WriteLine($"AddedDate: {device?.Device?.AddedDate}");
            Console.WriteLine($"Id: {device?.Id}");
        }

        public static void Print(this UserInvitationsResponse? userInvitations, bool outJson)
        {
            if (PrintJson(userInvitations, outJson))
            {
                return;
            }
            if (userInvitations?.UserInvitationInformations is null || !userInvitations.UserInvitationInformations.Any())
            {
                Console.WriteLine("No UserInvitations");
            }
            else
            {
                var count = 0;
                foreach (var userInvitation in userInvitations.UserInvitationInformations)
                {
                    Console.WriteLine($"------------ UserInvitation {++count} ----------");
                    userInvitation?.Print();
                }
            }                     
        }

        public static void Print(this UserInvitationResponse? userInvitation, bool outJson)
        {
            if (PrintJson(userInvitation, outJson))
            {
                return;
            }
            userInvitation?.UserInvitationInformation?.Print();
        }

        private static void Print(this UserInvitationInformation? userInvitation)
        {
            Console.WriteLine($"AllAppsVisible: {userInvitation?.UserInvitation?.AllAppsVisible}");
            Console.WriteLine($"Email: {userInvitation?.UserInvitation?.Email}");
            Console.WriteLine($"ExpirationDate: {userInvitation?.UserInvitation?.ExpirationDate}");
            Console.WriteLine($"FirstName: {userInvitation?.UserInvitation?.FirstName}");
            Console.WriteLine($"LastName: {userInvitation?.UserInvitation?.LastName}");
            Console.WriteLine($"ProvisioningAllowed: {userInvitation?.UserInvitation?.ProvisioningAllowed}");
            Console.WriteLine($"Id: {userInvitation?.Id}");
            if (userInvitation?.UserInvitation?.Roles?.Any() ?? false)
            {
                Console.WriteLine($"Roles:");
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                foreach (var role in userInvitation?.UserInvitation?.Roles)
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                {
                    Console.WriteLine($"  -{role}");
                }
            }
        }

        public static void Print(this UsersResponse? users, bool outJson)
        {
            if (PrintJson(users, outJson))
            {
                return;
            }
            if (users?.Users is null || !users.Users.Any())
            {
                Console.WriteLine("No Users");
            }
            else
            {
                var count = 0;
                foreach (var user in users.Users)
                {
                    Console.WriteLine($"------------ user {++count} ----------");
                    user?.Print();
                }
            }
        }

        public static void Print(this UserResponse? user, bool outJson)
        {
            if (PrintJson(user, outJson))
            {
                return;
            }
            user?.User?.Print();
        }

        private static void Print(this UserInformation? user)
        {
            Console.WriteLine($"AllAppsVisible: {user?.User?.AllAppsVisible}");
            Console.WriteLine($"FirstName: {user?.User?.FirstName}");
            Console.WriteLine($"LastName: {user?.User?.LastName}");
            Console.WriteLine($"ProvisioningAllowed: {user?.User?.ProvisioningAllowed}");
            Console.WriteLine($"Id: {user?.Id}");
            if (user?.User?.UserRoles?.Any() ?? false)
            {
                Console.WriteLine($"Roles:");
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                foreach (var role in user?.User?.UserRoles)
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                {
                    Console.WriteLine($"  -{role}");
                }
            }
        }

        public static void Print(this AppsResponse? appInformations, bool outJson)
        {
            if (PrintJson(appInformations, outJson))
            {
                return;
            }
            if (appInformations?.AppInformations is null || !appInformations.AppInformations.Any())
            {
                Console.WriteLine("No Apps");
            }
            else
            {
                var count = 0;
                foreach (var app in appInformations.AppInformations)
                {
                    Console.WriteLine($"------------ Apps {++count} ----------");
                    Console.WriteLine($"Name: {app?.App?.Name}");
                    Console.WriteLine($"BundleId: {app?.App?.BundleId}");
                    Console.WriteLine($"PrimaryLocale: {app?.App?.PrimaryLocale}");
                    Console.WriteLine($"Sku: {app?.App?.Sku}");
                }
            }            
        }

        private static void Print(this AppInformation? app)
        {
            Console.WriteLine($"Name: {app?.App?.Name}");
            Console.WriteLine($"BundleId: {app?.App?.BundleId}");
            Console.WriteLine($"PrimaryLocale: {app?.App?.PrimaryLocale}");
            Console.WriteLine($"Sku: {app?.App?.Sku}");
        }

        private static void Print(this Data? data)
        {
            Console.WriteLine($"Type: {data?.Type}");
            Console.WriteLine($"Id: {data?.Id}");
        }

        public static void Print(this BundleIdProfilesLinkagesResponse? datas, bool outJson)
        {
            if (PrintJson(datas, outJson))
            {
                return;
            }
            if (datas?.LinkedProfileIds is null || !datas.LinkedProfileIds.Any())
            {
                Console.WriteLine("No Profiles");
            }
            else
            {
                var count = 0;
                foreach (var data in datas.LinkedProfileIds)
                {
                    Console.WriteLine($"------------ Profile {++count} ----------");
                    data?.Print();
                }
            }            
        }

        public static void Print(this BundleIdBundleIdCapabilitiesLinkagesResponse? datas, bool outJson)
        {
            if (PrintJson(datas, outJson))
            {
                return;
            }
            if (datas?.BundleIdCapabilityIds is null || !datas.BundleIdCapabilityIds.Any())
            {
                Console.WriteLine("No Capabilities");
            }
            else
            {
                var count = 0;
                foreach (var data in datas.BundleIdCapabilityIds)
                {
                    Console.WriteLine($"------------ Capability {++count} ----------");
                    data?.Print();
                }
            }
        }

        public static void Print(this ProfileBundleIdLinkageResponse? datas, bool outJson)
        {
            if (PrintJson(datas, outJson))
            {
                return;
            }
            if (datas?.BundleIdId is null)
            {
                Console.WriteLine("No BundleId");
            }
            else
            {
                datas.BundleIdId?.Print();
            }
        }

        public static void Print(this ProfileCertificatesLinkagesResponse? datas, bool outJson)
        {
            if (PrintJson(datas, outJson))
            {
                return;
            }
            if (datas?.Certificates is null || !datas.Certificates.Any())
            {
                Console.WriteLine("No Certificates");
            }
            else
            {
                var count = 0;
                foreach (var data in datas.Certificates)
                {
                    Console.WriteLine($"------------ Certificate {++count} ----------");
                    data?.Print();
                }
            }
        }

        public static void Print(this ProfileDevicesLinkagesResponse? datas, bool outJson)
        {
            if (PrintJson(datas, outJson))
            {
                return;
            }
            if (datas?.Devices is null || !datas.Devices.Any())
            {
                Console.WriteLine("No Devices");
            }
            else
            {
                var count = 0;
                foreach (var data in datas.Devices)
                {
                    Console.WriteLine($"------------ Device {++count} ----------");
                    data?.Print();
                }
            }
        }

        public static void Print(this UserInvitationVisibleAppsLinkagesResponse? datas, bool outJson)
        {
            if (PrintJson(datas, outJson))
            {
                return;
            }
            if (datas?.LinkedVisibleApps is null || !datas.LinkedVisibleApps.Any())
            {
                Console.WriteLine("No Apps");
            }
            else
            {
                var count = 0;
                foreach (var data in datas.LinkedVisibleApps)
                {
                    Console.WriteLine($"------------ App {++count} ----------");
                    data?.Print();
                }
            }
        }

        public static void Print(this UserVisibleAppsLinkagesResponse? datas, bool outJson)
        {
            if (PrintJson(datas, outJson))
            {
                return;
            }
            if (datas?.LinkedVisibleApps is null || !datas.LinkedVisibleApps.Any())
            {
                Console.WriteLine("No Apps");
            }
            else
            {
                var count = 0;
                foreach (var data in datas.LinkedVisibleApps)
                {
                    Console.WriteLine($"------------ App {++count} ----------");
                    data?.Print();
                }
            }
        }

        public static void Print(this InternalErrorResponse err, bool outJson)
        {
            if(err.RawResponse is { })
            {
                err.RawResponse.Print(outJson);
                return;
            }
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(err.Message);
            if (err.Exception is { })
            {
                Console.WriteLine(err.Exception.ToString());
            }
        }

        public static void Print(this ErrorResponse err, bool outJson)
        {
            if (PrintJson(err, outJson))
            {
                return;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            if (err.Errors is { } && err.Errors.Any())
            {
                var count = 0;
                foreach (var error in err.Errors)
                {
                    Console.WriteLine($"------- Error {++count} -------");
                    Console.WriteLine($"Id: {error.Id}");
                    Console.WriteLine($"Code: {error.Code}");
                    Console.WriteLine($"Status: {error.Status}");
                    Console.WriteLine($"Title: {error.Title}");
                    Console.WriteLine($"Detail: {error.Detail}");
                    Console.WriteLine($"Source: {error.Source}");
                }
            }
        }

        private static void PrintJsonInternal<T>(T? res) where T : ApplicationResponse
        {
            Console.Write(JsonSerializer.Serialize(res, new JsonSerializerOptions { IgnoreNullValues = true }));
        }

        private static bool PrintJson<T>(T? res, bool outJson) where T : ApplicationResponse
        {
            if(outJson)
            {
                if(res is null)
                {
                    Console.Write("{}");
                } 
                else
                {
                    PrintJsonInternal(res);
                }
            }
            return outJson;
        }
    }
}
