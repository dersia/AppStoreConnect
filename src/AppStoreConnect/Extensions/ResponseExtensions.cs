using AppStoreConnect.Models.Utils;
using System.Net.Http;

namespace AppStoreConnect.Extensions
{
    public static class ResponseExtensions
    {
        public static PatternType ToPatternType(this HttpResponseMessage response) =>
            new PatternType
            {
                StatusCode = (int)response.StatusCode,
                Response = response
            };
    }
}
