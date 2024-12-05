//
// Copyright (c) 2024-2025 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot/blob/main/LICENSE
//

using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Karamem0.SwitchBot.Clients;

public class SwitchBotClient(
    ILoggerFactory loggerFactory,
    IHttpClientFactory httpClientFactory,
    string accessToken,
    string clientSecret
)
{

    private readonly ILogger logger = loggerFactory.CreateLogger<SwitchBotClient>();

    private readonly HttpClient httpClient = httpClientFactory.CreateClient("SwitchBot");

    private readonly string accessToken = accessToken;

    private readonly string clientSecret = clientSecret;

    public async Task<SwitchBotResponse> SendAsync(
        HttpMethod method,
        string uri,
        SwitchBotRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var span = DateTime.UtcNow - DateTime.UnixEpoch;
        var time = (long)span.TotalMilliseconds;
        var nonce = Guid
            .NewGuid()
            .ToString();
        var data = this.accessToken + time.ToString() + nonce;
        var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(this.clientSecret));
        var sign = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(data)));
        var httpRequestMessage = new HttpRequestMessage(method, uri)
        {
            Content = JsonContent.Create(request.Body)
        };
        httpRequestMessage.Headers.Add("Authorization", this.accessToken);
        httpRequestMessage.Headers.Add("Sign", sign);
        httpRequestMessage.Headers.Add("Nonce", nonce);
        httpRequestMessage.Headers.Add("T", time.ToString());
        var httpResponseMessage = await this.httpClient.SendAsync(httpRequestMessage, cancellationToken);
        var httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync(cancellationToken);
        this.logger.LogDebug("StatusCode: {StatusCode}", httpResponseMessage.StatusCode);
        this.logger.LogDebug("Content: {Content}", httpResponseContent);
        if (httpResponseMessage.IsSuccessStatusCode)
        {
            var httpResponseData = await httpResponseMessage.Content.ReadFromJsonAsync<SwitchBotResponse>(cancellationToken);
            if (httpResponseData?.StatusCode == 100)
            {
                return httpResponseData;
            }
            throw new InvalidOperationException(httpResponseData?.Message);
        }
        else
        {
            throw new HttpRequestException(
                httpResponseMessage.ReasonPhrase,
                null,
                httpResponseMessage.StatusCode
            );
        }
    }

}
