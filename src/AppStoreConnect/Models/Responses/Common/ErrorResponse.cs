using AppStoreConnect.Models.Pocos.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AppStoreConnect.Models.Responses.Common
{
    public class ErrorResponse : ApplicationResponse
    {
        [JsonPropertyName("errors")]
        public List<Error>? Errors { get; set; }
    }
}
