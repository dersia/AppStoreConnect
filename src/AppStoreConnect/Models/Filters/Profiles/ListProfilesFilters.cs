using AppStoreConnect.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppStoreConnect.Models.Filters.Profiles
{
    public class ListProfilesFilters
    {
        public List<string>? FieldsCertificatesFilter { get; set; }
        public List<string>? FieldsDevicesFilter { get; set; }
        public List<string>? FieldsProfilesFilter { get; set; }
        public List<string>? FieldsBundleIdsFilter { get; set; }
        public int? LimitFilter { get; set; }
        public int? LimitCertificatesFilter { get; set; }
        public int? LimitDevicesFilter { get; set; }
        public List<string>? IdFilter { get; set; }
        public List<string>? NameFilter { get; set; }
        public List<ProfileState>? ProfileStateFilter { get; set; }
        public List<ProfileType>? ProfileTypeFilter { get; set; }
        public List<string>? SortBy { get; set; }
        public List<string>? IncludeFilter { get; set; }


        public static class Keys
        {
            public const string FieldsDevices = "&fields[devices]";
            public const string FieldsCertificates = "&fields[certificates]";
            public const string FieldsProfiles = "&fields[profiles]";
            public const string FieldsBundleIds = "&fields[bundleIds]";
            public const string Include = "&include";
            public const string Limit = "&limit";
            public const string LimitDevices = "&limit[devices]";
            public const string LimitCertificates = "&limit[certificates]";
            public const string FilterIds = "&filter[id]";
            public const string FilterNames = "&filter[name]";
            public const string FilterProfileStates = "&filter[profileState]";
            public const string FilterProfileTypes = "&filter[profileType]";
            public const string Sort = "&sort";
        }

        public static class FieldsDevices
        {
            public const string AddedDate = "addedDate";
            public const string DeviceClass = "deviceClass";
            public const string Model = "model";
            public const string Name = "name";
            public const string Platform = "platform";
            public const string Status = "status";
            public const string Udid = "udid";
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

        public static class FieldsProfiles
        {
            public const string BundleId = "bundleId";
            public const string Certificates = "certificates";
            public const string CreatedDate = "createdDate";
            public const string Devices = "devices";
            public const string ExpirationDate = "expirationDate";
            public const string Name = "name";
            public const string Platform = "platform";
            public const string ProfileContent = "profileContent";
            public const string ProfileState = "profileState";
            public const string ProfileType = "profileType";
            public const string Uuid = "uuid";
        }

        public static class FieldsBundleIds
        {
            public const string BundleIdCapabilities = "bundleIdCapabilities";
            public const string Identifier = "identifier";
            public const string Name = "name";
            public const string Platform = "platform";
            public const string Profiles = "profiles";
            public const string SeedId = "seedId";
        }

        public static class Sort
        {
            public const string Id = "id";
            public const string IdDesc = "-id";
            public const string Name = "name";
            public const string NameDesc = "-name";
            public const string ProfileState = "profileState";
            public const string ProfileStateDesc = "-profileState";
            public const string ProfileType = "profileType";
            public const string ProfileTypeDesc = "-profileType";
        }

        public static class Include
        {
            public const string bundleId = "bundleId";
            public const string certificates = "certificates";
            public const string devices = "devices";
        }
    }
}
