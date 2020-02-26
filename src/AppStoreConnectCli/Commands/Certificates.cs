using AppStoreConnect.Models.Enums;
using AppStoreConnect.Models.Pocos.Certificates;
using AppStoreConnect.Models.Requests.Certificates;
using AppStoreConnect.Models.Responses.Certificates;
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
    public static class Certificates
    {
        public static Command CreateCertificates()
        {
            var create = new Command("create", "create a new certificate");
            create.AddAlias("c");
            create.AddArgument(new Argument<string>("csrContent"));
            create.AddOption(new Option<CertificateTypes>("--type") { Argument = new Argument<CertificateTypes> { Arity = ArgumentArity.ExactlyOne } });
            create.AddSubCommandArgument();
            create.Handler = CommandHandler.Create(typeof(Certificates).GetMethod(nameof(CreateFromCsr)));

            var createFromFile = new Command("createFromFile", "create a new certificate from certificate json file");
            createFromFile.AddAlias("cfile");
            createFromFile.AddAlias("cf");
            createFromFile.AddOption(new Option<CertificateTypes>("--type") { Argument = new Argument<CertificateTypes> { Arity = ArgumentArity.ExactlyOne } });
            createFromFile.AddSubCommandArgument();
            createFromFile.AddArgument(new Argument<FileInfo>("file"));
            createFromFile.Handler = CommandHandler.Create(typeof(Certificates).GetMethod(nameof(CreateFromCsrFile)));

            var get = new Command("get", "get a certificate by its id");
            get.AddAlias("g");
            get.AddArgument(new Argument<string>("certificateId"));
            get.AddSubCommandArgument();
            get.Handler = CommandHandler.Create(typeof(Certificates).GetMethod(nameof(Get)));

            var getNoContent = new Command("getEntry", "get a certificate without its content by its id");
            getNoContent.AddAlias("ge");
            getNoContent.AddArgument(new Argument<string>("certificateId"));
            getNoContent.AddSubCommandArgument();
            getNoContent.Handler = CommandHandler.Create(typeof(Certificates).GetMethod(nameof(GetEntry)));

            var getContent = new Command("getContent", "get a certificate content by its id");
            getContent.AddAlias("gc");
            getContent.AddArgument(new Argument<string>("certificateId"));
            getContent.AddSubCommandArgument();
            getContent.Handler = CommandHandler.Create(typeof(Certificates).GetMethod(nameof(GetContent)));

            var list = new Command("list", "list all certificates (no content)");
            list.AddAlias("l");
            list.AddSubCommandArgument();
            list.Handler = CommandHandler.Create(typeof(Certificates).GetMethod(nameof(List)));

            var revoke = new Command("revoke", "revoke a certificate by its id");
            revoke.AddAlias("r");
            revoke.AddArgument(new Argument<string>("certificateId"));
            revoke.AddSubCommandArgument();
            revoke.Handler = CommandHandler.Create(typeof(Certificates).GetMethod(nameof(Revoke)));

            var certificates = new Command("certificates", "create, get, list or revoke certificates")
            {
                create,
                createFromFile,
                get,
                getContent,
                getNoContent,
                list,
                revoke
            };
            return certificates;
        }

        public static async Task Revoke(string certificateId, string token, bool outJson)
        {
            var result = await token.GetClient().Certificates.RevokeCertificate(certificateId);
            result.Handle<NoContentResponse>(res =>
            {
                res.Print(() => Console.WriteLine($"Revoked certificate '{certificateId}'"), outJson);
            }, outJson);
        }

        public static async Task List(string token, bool outJson)
        {
            var result = await token.GetClient().Certificates.ListCertificates();
            result.Handle<CertificatesResponse>(res =>
            {
                res.Print(outJson);
            }, outJson);
        }

        public static async Task GetContent(string certificateId, string token, bool outJson)
        {
            var result = await token.GetClient().Certificates.GetCertificate(certificateId);
            result.Handle<CertificateResponse>(res =>
            {
                Console.WriteLine(res.CertificateInformation?.Certificate?.CertificateContent);
            }, outJson);
        }

        public static async Task GetEntry(string certificateId, string token, bool outJson)
        {
            var result = await token.GetClient().Certificates.GetCertificate(certificateId);
            result.Handle<CertificateResponse>(res =>
            {
                res.Print(outJson, false);
            }, outJson);
        }

        public static async Task Get(string certificateId, string token, bool outJson)
        {
            var result = await token.GetClient().Certificates.GetCertificate(certificateId);
            result.Handle<CertificateResponse>(res =>
            {
                res.Print(outJson);
            }, outJson);
        }

        public static async Task CreateFromCsrFile(CertificateTypes type, FileInfo file, string token, bool outJson)
        {
            var csr = await file.OpenText().ReadToEndAsync();
            await CreateFromCsr(csr, type, token, outJson);
        }

        public static async Task CreateFromCsr(string csrContent, CertificateTypes type, string token, bool outJson)
        {
            if (string.IsNullOrWhiteSpace(csrContent))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Csr is empty");
                return;
            }
            var payload = new CertificateCreateRequest()
            {
                CsrInformation = new CsrInformation
                {
                    Csr = new AppStoreConnect.Models.Pocos.Certificates.Csr
                    {
                        CertificateType = type,
                        CsrContent = csrContent
                    },
                    Type = ResourceTypes.certificates
                }
            };
            var result = await token.GetClient().Certificates.CreateCertificate(payload);
            result.Handle<CertificateResponse>(res =>
            {
                res.Print(outJson, false);
            }, outJson);
        }
    }
}
