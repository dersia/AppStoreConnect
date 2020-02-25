using AppStoreConnect.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppStoreConnect.Models.Filters.Certificates
{
    public class ListCertificatesFilters
    {
        public List<string>? FieldsCertificatesFilter { get; set; }
        public List<string>? IdFilter { get; set; }
        public List<string>? SerialNumberFilter { get; set; }
        public List<CertificateTypes>? CertificateTypeFilter { get; set; }
        public List<string>? DisplayNameFilter { get; set; }
        public int? LimitFilter { get; set; }
        public List<string>? SortBy { get; set; }


        public static class Keys
        {
            public const string FieldsCertificates = "&fields[certificates]";
            public const string FilterId = "&filter[id]";
            public const string FilterSerialNumber = "&filter[serialNumber]";
            public const string FilterCertificateType = "&filter[certificateType]";
            public const string FilterDisplayName = "&filter[displayName]";
            public const string Sort = "&sort";
            public const string Limit = "&limit";
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

        public static class Sort
        {
            public const string Id = "id";
            public const string IdDesc = "-id";
            public const string DisplayName = "displayName";
            public const string DisplayNameDesc = "-displayName";
            public const string CertificateType = "certificateType";
            public const string CertificateTypeDesc = "-certificateType";
            public const string SerialNumber = "serialNumber";
            public const string SerialNumberDesc = "-serialNumber";
        }
    }
}
