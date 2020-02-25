using AppStoreConnect.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppStoreConnect.Models.Filters.Certificates
{
    public class GetCertificateFilters
    {
        public List<string>? FieldsCertificatesFilter { get; set; }


        public static class Keys
        {
            public const string FieldsCertificates = "&fields[certificates]";
        }

        public static class FieldsCertificates
        {
            public const string CertificateContent = "certificateContent";
            public const string CertificateType = "certificateType";
            public const string CsrContent = "csrContent";
            public const string DisplayName = "displayName";
            public const string ExpirationDate = "expirationDate";
            public const string Name = "name";
            public const string Platform = "platform";
            public const string SerialNumber = "serialNumber";
        }
    }
}
