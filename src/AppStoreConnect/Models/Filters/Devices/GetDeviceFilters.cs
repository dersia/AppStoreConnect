using AppStoreConnect.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppStoreConnect.Models.Filters.Devices
{
    public class GetDeviceFilters
    {
        public List<string>? FieldsDevicesFilter { get; set; }


        public static class Keys
        {
            public const string FieldsDevices = "&fields[devices]";
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
    }
}
