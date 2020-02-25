using AppStoreConnect.Models.Pocos.Apps;
using AppStoreConnect.Models.Pocos.BundleIdCapabilities;
using AppStoreConnect.Models.Pocos.BundleIds;
using AppStoreConnect.Models.Pocos.Certificates;
using AppStoreConnect.Models.Pocos.Common;
using AppStoreConnect.Models.Pocos.Devices;
using AppStoreConnect.Models.Pocos.Profiles;
using AppStoreConnect.Models.Pocos.UserInvitations;
using AppStoreConnect.Models.Pocos.Users;
using AppStoreConnect.Models.Responses.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppStoreConnectCli.Common
{
    public static class RendererExtension
    {
        public static void Print(this BundleIdInformation? bundleId)
        {
            Console.WriteLine($"Name: {bundleId?.BundleId?.Name}");
            Console.WriteLine($"Identifier: {bundleId?.BundleId?.Identifier}");
            Console.WriteLine($"Platform: {bundleId?.BundleId?.Platform}");
            Console.WriteLine($"SeedId: {bundleId?.BundleId?.SeedId}");
            Console.WriteLine($"Id: {bundleId?.Id}");
        }

        public static void Print(this BundleIdCapabilityInformation? capability)
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

        public static void Print(this ProfileInformation? profile, bool withContent = true)
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

        public static void Print(this CertificateInformation? certificate, bool withContent = true)
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

        public static void Print(this DeviceInformation? device)
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

        public static void Print(this UserInvitationInformation? userInvitation)
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

        public static void Print(this UserInformation? user)
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

        public static void Print(this AppInformation? appInformation)
        {
            Console.WriteLine($"Name: {appInformation?.App?.Name}");
            Console.WriteLine($"BundleId: {appInformation?.App?.BundleId}");
            Console.WriteLine($"PrimaryLocale: {appInformation?.App?.PrimaryLocale}");
            Console.WriteLine($"Sku: {appInformation?.App?.Sku}");
        }

        public static void Print(this Data? data)
        {
            Console.WriteLine($"Type: {data?.Type}");
            Console.WriteLine($"Id: {data?.Id}");
        }

        public static void Print(this InternalErrorResponse err)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(err.Message);
            if (err.Exception is null)
            {
                Console.WriteLine(err.ToString());
            }
            else
            {
                Console.WriteLine(err.Exception.ToString());
            }
        }

        public static void Print(this ErrorResponse err)
        {
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
    }
}
