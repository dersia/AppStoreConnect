using System.Text.Json;
using System.Text.Json.Serialization;

namespace AppStoreConnect.Models.Requests.BundleIds
{
    public class BundleIdCreate
    {
        [JsonPropertyName("bundleIds")]
        public string? BundleIds { get; set; }
        
    }
}