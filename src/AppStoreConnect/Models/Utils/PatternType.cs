using AppStoreConnect.Models.Responses.Common;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace AppStoreConnect.Models.Utils
{
    public class PatternType
    {
        public int StatusCode { get; set; }
        public HttpResponseMessage Response { get; set; }
    }
}
