using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AppStoreConnect.Models.Responses.Common
{
    public class InternalErrorResponse : ApplicationResponse
    {
        public InternalErrorResponse(string? message = null, Exception? exception = null, string? rawBody = null, ErrorResponse? rawResponse = null)
        {
            Message = message;
            Exception = exception;
            RawBody = rawBody;
            RawResponse = rawResponse;
        }

        public static async Task<InternalErrorResponse> CreateFromResponseMessage(HttpResponseMessage? httpMessage)
        {
            var message = string.Empty;
            var rawBody = string.Empty;
            ErrorResponse? rawResponse = null;
            if (httpMessage is { })
            {
                rawBody = httpMessage.Content is null ? string.Empty : await httpMessage.Content.ReadAsStringAsync();
                message = $"Response: {(int)httpMessage.StatusCode} ({httpMessage.StatusCode}){Environment.NewLine}{rawBody}";
            }
            try
            {
                rawResponse = JsonSerializer.Deserialize<ErrorResponse>(rawBody, new JsonSerializerOptions { IgnoreNullValues = true });
            }
            catch { }
            return new InternalErrorResponse(message, new Exception(message), rawBody, rawResponse);
        }

        public Exception? Exception { get; set; }
        public string? Message { get; set; }
        public string? RawBody { get; set; }
        public ErrorResponse? RawResponse { get; set; }
    }
}
