using AppStoreConnect.Abstractions;
using AppStoreConnect.Constants;
using AppStoreConnect.Endpoints.Base;
using AppStoreConnect.Extensions;
using AppStoreConnect.Models.Requests.Devices;
using AppStoreConnect.Models.Responses.Common;
using AppStoreConnect.Models.Responses.Devices;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AppStoreConnect.Endpoints.Devices
{
    public partial class Devices : EndpointBase, IDevices
    {
        public async Task<ApplicationResponse> UpdateDevice(string id, DeviceUpdateRequest payload)
        {
            var endpointUrl = $"{EndpointConstants.AppStoreConnectBaseUrl}/{EndpointConstants.Enpoints.Devices}/{id}";
            var body = new StringContent(JsonSerializer.Serialize(payload, new JsonSerializerOptions { IgnoreNullValues = true }));
            body.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(EndpointConstants.JsonContentType);
            var result = await Client.PatchAsync(endpointUrl, body);
            return result.ToPatternType() switch
            {
                { StatusCode: 200 } br => JsonSerializer.Deserialize<DeviceResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 400 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 403 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 404 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 409 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: _ } res => await InternalErrorResponse.CreateFromResponseMessage(res.Response),
            };
        }
    }
}
