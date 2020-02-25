using AppStoreConnect;
using AppStoreConnect.Jwt;
using AppStoreConnect.Models.Responses.BundleIds;
using AppStoreConnect.Models.Responses.Common;
using System;
using System.CommandLine;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.CommandLine.Invocation;
using AppStoreConnectCli.Commands;
using System.CommandLine.Builder;

namespace AppStoreConnectCli
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var command = new RootCommand();
            command.AddCommand(BundleIds.CreateBundleIds());
            command.AddCommand(BundleIdCapabilities.CreateBundleIdCapabilities());
            command.AddCommand(Certificates.CreateCertificates());
            command.AddCommand(Devices.CreateDevices());
            command.AddCommand(Profiles.CreateProfiles());
            command.AddCommand(UserInvitations.CreateUserInvitations());
            command.AddCommand(Users.CreateUsers());
            command.AddCommand(Tools.CreateTools());
            await command.InvokeAsync(args);
        }
    }
}
