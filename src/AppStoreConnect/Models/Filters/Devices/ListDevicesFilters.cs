using AppStoreConnect.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppStoreConnect.Models.Filters.Devices
{
    public class ListDevicesFilters
    {
        public List<string>? FieldsDevicesFilter { get; set; }
        public List<string>? IdFilter { get; set; }
        public List<string>? NameFilter { get; set; }
        public List<BundleIdPlatform>? PlatformFilter { get; set; }
        public List<DeviceStatus>? StatusFilter { get; set; }
        public List<string>? UdidFilter { get; set; }
        public int? LimitFilter { get; set; }
        public List<string>? SortBy { get; set; }


        public static class Keys
        {
            public const string FieldsDevices = "&fields[devices]";
            public const string FilterId = "&filter[id]";
            public const string FilterName = "&filter[name]";
            public const string FilterPlatform = "&filter[platform]";
            public const string FilterStatus = "&filter[status]";
            public const string FilterUdid = "&filter[udid]";
            public const string Sort = "&sort";
            public const string Limit = "&limit";
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

        public static class Sort
        {
            public const string Id = "id";
            public const string IdDesc = "-id";
            public const string Name = "name";
            public const string NameDesc = "-name";
            public const string Platform = "platform";
            public const string PlatformDesc = "-platform";
            public const string Status = "status";
            public const string StatusDesc = "-status";
            public const string Udid = "udid";
            public const string UdidDesc = "-udid";
        }
    }
}
