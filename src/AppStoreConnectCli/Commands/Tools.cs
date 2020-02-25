using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;

namespace AppStoreConnectCli.Commands
{
    public static class Tools
    {
        public static Command CreateTools() 
            => new Command("tools", "Tools for certification and jwt handling")
            {
                Jwt.CreateJwt(),
                Cert.CreateCert()
            };
    }
}
