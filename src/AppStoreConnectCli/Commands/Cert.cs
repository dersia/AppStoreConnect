using AppStoreConnectCli.CertificateTools;
using Org.BouncyCastle.Pkcs;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AppStoreConnectCli.Commands
{
    public static class Cert
    {
        public static Command CreateCert()
        {
            var csr = new Command("csr");
            csr.AddArgument(new Argument<string>("commonName"));
            csr.AddArgument(new Argument<string>("countryCode"));
            csr.AddOption(new Option<string?>("--emailAddress") { Argument = new Argument<string?> { Arity = ArgumentArity.ZeroOrOne } });
            csr.AddOption(new Option<string?>("--state") { Argument = new Argument<string?> { Arity = ArgumentArity.ZeroOrOne } });
            csr.AddOption(new Option<string?>("--city") { Argument = new Argument<string?> { Arity = ArgumentArity.ZeroOrOne } });
            csr.AddOption(new Option<string?>("--company") { Argument = new Argument<string?> { Arity = ArgumentArity.ZeroOrOne } });
            csr.AddOption(new Option<string?>("--unit") { Argument = new Argument<string?> { Arity = ArgumentArity.ZeroOrOne } });
            csr.AddOption(new Option<int?>("--keyLength", () => 2048) { Argument = new Argument<int?> { Arity = ArgumentArity.ZeroOrOne } });
            csr.AddOption(new Option<FileInfo?>(new[] { "--out-file", "-o" }) { Argument = new Argument<FileInfo?> { Arity = ArgumentArity.ZeroOrOne } });
            csr.Handler = CommandHandler.Create(typeof(Cert).GetMethod("CreateCsr"));

            var csrInteractive = new Command("csri");
            csrInteractive.AddOption(new Option<int?>("--keyLength", () => 2048) { Argument = new Argument<int?> { Arity = ArgumentArity.ZeroOrOne } });
            csrInteractive.AddOption(new Option<FileInfo?>(new[] { "--out-file", "-o" }) { Argument = new Argument<FileInfo?> { Arity = ArgumentArity.ZeroOrOne } });
            csrInteractive.Handler = CommandHandler.Create(async (FileInfo? outFile, int? keyLength, bool? outBase64) => 
            {
                var csr = new CertUtil().CreateCertificateSigningRequestInteractive();
                await OutPut(csr, outFile);
            });

            var p12 = new Command("p12");
            p12.AddArgument(new Argument<string>("cer"));
            p12.AddOption(new Option<string?>("--password") { Argument = new Argument<string?> { Arity = ArgumentArity.ZeroOrOne } });
            p12.AddOption(new Option<FileInfo?>(new[] { "--out-file", "-o" }) { Argument = new Argument<FileInfo?> { Arity = ArgumentArity.ZeroOrOne } });
            p12.Handler = CommandHandler.Create(async (string cer, string? password, FileInfo? outFile, bool? outBase64) => 
            {
                var p12 = new CertUtil().CreateP12(Encoding.UTF8.GetBytes(cer), password);
                await OutPut(p12, outFile);
            });

            var p12FromFile = new Command("p12FromFile");
            p12FromFile.AddArgument(new Argument<FileInfo>("cer-file"));
            p12FromFile.AddOption(new Option<string?>("--password") { Argument = new Argument<string?> { Arity = ArgumentArity.ZeroOrOne } });
            p12FromFile.AddOption(new Option<FileInfo?>(new[] { "--out-file", "-o" }) { Argument = new Argument<FileInfo?> { Arity = ArgumentArity.ZeroOrOne } });
            p12FromFile.Handler = CommandHandler.Create(async (FileInfo cerFile, string? password, FileInfo? outFile, bool? outBase64) =>
            {                
                var p12 = new CertUtil().CreateP12(File.ReadAllBytes(cerFile.FullName), password);
                await OutPut(p12, outFile);
            });

            var cert = new Command("cert", "Create 'Certificate Signing Request (CSR)' or Convert CER to P12 (PKCS12)")
            {
                csr,
                csrInteractive,
                p12,
                p12FromFile
            };
            return cert;
        }

        public static async Task CreateCsr(string? emailAddress, string commonName, string countryCode, string? state, string? city, string? company, string? unit, int? keyLength, FileInfo? outFile)
        {
            var csr = new CertUtil().CreateCertificateSigningRequest(commonName, countryCode, emailAddress, state, city, company, unit, keyLength ?? 2048);
            await OutPut(csr, outFile);
        }

        private static async Task OutPut(byte[] rawData, FileInfo? outFile)
        {
            if (outFile is { })
            {                
                if (outFile.Exists)
                {
                    File.Delete(outFile.FullName);
                }
                await File.WriteAllBytesAsync(outFile.FullName, rawData);
            }
            else
            {
                Console.WriteLine(Convert.ToBase64String(rawData));
            }
        }

        private static async Task OutPut(Pkcs10CertificationRequest csr, FileInfo? outFile)
        {
            if (outFile is { })
            {

                if (outFile.Exists)
                {
                    File.Delete(outFile.FullName);
                }
                using var outStream = outFile.Open(FileMode.CreateNew, FileAccess.Write);
                using var textWriter = new StreamWriter(outStream);
                var pemWriter = new Org.BouncyCastle.OpenSsl.PemWriter(textWriter);
                pemWriter.WriteObject(csr);
                await pemWriter.Writer.FlushAsync();
            }
            else
            {
                var sb = new StringBuilder();
                using var textWriter = new StringWriter(sb);
                var pemWriter = new Org.BouncyCastle.OpenSsl.PemWriter(textWriter);
                pemWriter.WriteObject(csr);
                await pemWriter.Writer.FlushAsync();
                Console.WriteLine(sb.ToString());
            }
        }
    }
}
