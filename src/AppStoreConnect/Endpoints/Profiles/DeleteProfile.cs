﻿using AppStoreConnect.Abstractions;
using AppStoreConnect.Constants;
using AppStoreConnect.Endpoints.Base;
using AppStoreConnect.Extensions;
using AppStoreConnect.Models.Requests.Profiles;
using AppStoreConnect.Models.Responses.Common;
using AppStoreConnect.Models.Responses.Profiles;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AppStoreConnect.Endpoints.Profiles
{
    public partial class Profiles : EndpointBase, IProfiles
    {
        public async Task<ApplicationResponse> DeleteProfile(string id)
        {
            var endpointUrl = $"{EndpointConstants.AppStoreConnectBaseUrl}/{EndpointConstants.Enpoints.Profiles}/{id}";
            var result = await Client.DeleteAsync(endpointUrl);
            return result.ToPatternType() switch
            {
                { StatusCode: 204 } br => new NoContentResponse(),
                { StatusCode: 400 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 403 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 404 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: 409 } br => JsonSerializer.Deserialize<ErrorResponse>(await br.Response.Content.ReadAsStringAsync()),
                { StatusCode: _ } res => await InternalErrorResponse.CreateFromResponseMessage(res.Response),
            };
        }
    }
}
