using AppStoreConnect;
using AppStoreConnect.Jwt;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.Text;

namespace AppStoreConnectCli.Commands
{
    public static class Jwt
    {
        public static Command CreateJwt()
        {
            var fromFile = new Command("fromFile")
            {

            };
            AddFromFileArguments(fromFile);
            fromFile.Handler = CommandHandler.Create(
                (FileSystemInfo p8Path, string kid, string issuer, string? audience, FileSystemInfo configFile) =>
                {
                    var result = KeyUtils.CreateTokenAndSign(p8Path.FullName, kid, issuer, audience ?? "appstoreconnect-v1");
                    Console.WriteLine($"{result}");
                }
            );
            var fromConfig = new Command("fromConfig")
            {

            };
            AddFromConfigArguments(fromConfig);
            var fromKeyVault = new Command("fromKeyVault")
            {

            };
            AddFromKeyVaultArguments(fromKeyVault);
            var jwt = new Command("jwt", "create a new jwt token from certificate file, config or Azure KeyVault")
            {
                fromFile,
                //fromConfig,
                //fromKeyVault
            };
            return jwt;
        }

        private static Command AddFromFileArguments(Command command)
        {
            command.AddArgument(new Argument<FileInfo>("p8Path"));
            command.AddArgument(new Argument<string>("kid"));
            command.AddArgument(new Argument<string>("issuer"));
            command.AddOption(new Option<string?>("--audience") { Argument = new Argument<string?> { Arity = ArgumentArity.ZeroOrOne } });
            return command;
        }

        private static Command AddFromConfigArguments(Command command)
        {
            command.AddArgument(new Argument<FileInfo>("config-file-path"));
            return command;
        }

        private static Command AddFromKeyVaultArguments(Command command)
        {
            command.AddArgument(new Argument<Uri>("p8Uri"));
            command.AddArgument(new Argument<Uri>("kidUri"));
            command.AddArgument(new Argument<Uri>("issuerUri"));
            command.AddArgument(new Argument<Uri>("audienceUri"));
            command.AddOption(new Option<string>("--username"));
            command.AddOption(new Option<string>("--password"));
            return command;
        }
    }
}
