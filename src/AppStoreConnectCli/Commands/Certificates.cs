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
            create.AddArgument(new Argument<CertificateTypes>("type"));
            create.AddSubCommandArgument();
            create.Handler = CommandHandler.Create(
                async (string csrContent, CertificateTypes type, string token) => await CreateFromCsr(type, csrContent, token));

            var createFromFile = new Command("createFromFile", "create a new certificate from certificate json file");
            createFromFile.AddAlias("cfile");
            createFromFile.AddAlias("cf");
            createFromFile.AddArgument(new Argument<CertificateTypes>("type"));
            createFromFile.AddSubCommandArgument();
            createFromFile.AddArgument(new Argument<FileInfo>("file"));
            createFromFile.Handler = CommandHandler.Create(async (CertificateTypes type, FileInfo file, string token) => 
            {
                var csr = await file.OpenText().ReadToEndAsync();
                await CreateFromCsr(type, csr, token);
            });

            var get = new Command("get", "get a certificate by its id");
            get.AddAlias("g");
            get.AddArgument(new Argument<string>("certificateId"));
            get.AddSubCommandArgument();
            get.Handler = CommandHandler.Create(async (string certificateId, string token) => 
            {
                var result = await token.GetClient().Certificates.GetCertificate(certificateId);
                result.Handle<CertificateResponse>(res =>
                {
                    res.CertificateInformation?.Print();
                });
            });

            var getNoContent = new Command("getEntry", "get a certificate without its content by its id");
            getNoContent.AddAlias("ge");
            getNoContent.AddArgument(new Argument<string>("certificateId"));
            getNoContent.AddSubCommandArgument();
            getNoContent.Handler = CommandHandler.Create(async (string certificateId, string token) =>
            {
                var result = await token.GetClient().Certificates.GetCertificate(certificateId);
                result.Handle<CertificateResponse>(res =>
                {
                    res.CertificateInformation?.Print();
                });
            });

            var getContent = new Command("getContent", "get a certificate content by its id");
            getContent.AddAlias("gc");
            getContent.AddArgument(new Argument<string>("certificateId"));
            getContent.AddSubCommandArgument();
            getContent.Handler = CommandHandler.Create(async (string certificateId, string token) =>
            {
                var result = await token.GetClient().Certificates.GetCertificate(certificateId);
                result.Handle<CertificateResponse>(res =>
                {
                    Console.WriteLine(res.CertificateInformation?.Certificate?.CertificateContent);
                });
            });

            var list = new Command("list", "list all certificates (no content)");
            list.AddAlias("l");
            list.AddSubCommandArgument();
            list.Handler = CommandHandler.Create(async (string token) =>
            {
                var result = await token.GetClient().Certificates.ListCertificates();
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
                            Console.WriteLine($"------------ Cert {++count} ----------");
                            certificate?.Print(false);
                        }
                    }
                });
            });

            var revoke = new Command("revoke", "revoke a certificate by its id");
            revoke.AddAlias("r");
            revoke.AddArgument(new Argument<string>("certificateId"));
            revoke.AddSubCommandArgument();
            revoke.Handler = CommandHandler.Create(async (string certificateId, string token) =>
            {
                var result = await token.GetClient().Certificates.RevokeCertificate("certificateId");
                result.Handle<NoContentResponse>(res =>
                {
                    Console.WriteLine($"Revoked certificate '{certificateId}'");
                });
            });

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

        private static async Task CreateFromCsr(CertificateTypes? type, string? csr, string token)
        {
            if (string.IsNullOrWhiteSpace(csr))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Csr is empty");
                return;
            }
            if (type is null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("CertificateType is missing");
                return;
            }
            var payload = new CertificateCreateRequest()
            {
                CsrInformation = new CsrInformation
                {
                    Csr = new AppStoreConnect.Models.Pocos.Certificates.Csr
                    {
                        CertificateType = type,
                        CsrContent = csr
                    },
                    Type = ResourceTypes.certificates
                }
            };
            var result = await token.GetClient().Certificates.CreateCertificate(payload);
            result.Handle<CertificateResponse>(res =>
            {
                res.CertificateInformation?.Print(false);
            });
        }
    }
}
