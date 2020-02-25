using System;
using System.Collections.Generic;
using System.Text;

namespace AppStoreConnect.Models.Responses.Common
{
    public class InternalErrorResponse : ApplicationResponse
    {
        public Exception? Exception { get; set; }
        public string? Message { get; set; }
    }
}
