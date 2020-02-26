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
            var fromFile = new Command("fromFile", "Create a Bearer Token from p8 cdrtificate, keyId and issuerId");
            fromFile.AddArgument(new Argument<FileInfo>("p8Path"));
            fromFile.AddArgument(new Argument<string>("kid"));
            fromFile.AddArgument(new Argument<string>("issuer"));
            fromFile.AddOption(new Option<string?>("--audience") { Argument = new Argument<string?> { Arity = ArgumentArity.ZeroOrOne } });
            fromFile.Handler = CommandHandler.Create(typeof(Jwt).GetMethod(nameof(FromFile)));
            
            var fromConfig = new Command("fromConfig");
            fromConfig.AddArgument(new Argument<FileInfo>("config-file-path")); 
            fromConfig.Handler = CommandHandler.Create(typeof(Jwt).GetMethod(nameof(FromConfig)));

            var fromKeyVault = new Command("fromKeyVault");
            fromKeyVault.AddArgument(new Argument<Uri>("p8Uri"));
            fromKeyVault.AddArgument(new Argument<Uri>("kidUri"));
            fromKeyVault.AddArgument(new Argument<Uri>("issuerUri"));
            fromKeyVault.AddOption(new Option<Uri?>("--audience") { Argument = new Argument<Uri?> { Arity = ArgumentArity.ZeroOrOne } });
            fromKeyVault.AddOption(new Option<string>("--username") { Argument = new Argument<string> { Arity = ArgumentArity.ExactlyOne } });
            fromKeyVault.AddOption(new Option<string>("--password") { Argument = new Argument<string> { Arity = ArgumentArity.ExactlyOne } });
            fromKeyVault.Handler = CommandHandler.Create(typeof(Jwt).GetMethod(nameof(FromKeyVault)));

            var jwt = new Command("jwt", "create a new jwt token from certificate file, config or Azure KeyVault")
            {
                fromFile,
                //fromConfig,
                //fromKeyVault
            };
            return jwt;
        }

        public static void FromFile(FileSystemInfo p8Path, string kid, string issuer, string? audience)
        {
            var result = KeyUtils.CreateTokenAndSign(p8Path.FullName, kid, issuer, audience ?? "appstoreconnect-v1");
            Console.WriteLine($"{result}");
        }

        public static void FromConfig(FileSystemInfo configFilePath)
        {
            Console.WriteLine($"");
        }

        public static void FromKeyVault(Uri p8Uri, Uri kidUri, Uri issuerUri, Uri? audienceUri, string password, string username)
        {
            Console.WriteLine($"");
        }
    }
}
