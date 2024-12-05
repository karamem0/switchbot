//
// Copyright (c) 2024-2025 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot/blob/main/LICENSE
//

using Karamem0.SwitchBot.Clients;
using Karamem0.SwitchBot.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;

namespace Karamem0.SwitchBot.Services;

public interface ISwitchBotService
{

    Task CreateWebhookAsync(
        Uri url,
        string deviceList,
        CancellationToken cancellationToken
    );

    Task DeleteWebhookAsync(Uri url, CancellationToken cancellationToken);

    Task<WebhookDetails[]> GetWebhookDetailsAsync(Uri[] urls, CancellationToken cancellationToken);

    Task<Uri[]> GetWebhookUrlsAsync(CancellationToken cancellationToken);

}

public class SwitchBotService(SwitchBotClient switchBotClient) : ISwitchBotService
{

    private readonly SwitchBotClient switchBotClient = switchBotClient;

    public async Task CreateWebhookAsync(
        Uri url,
        string deviceList,
        CancellationToken cancellationToken = default
    )
    {
        _ = await this
            .switchBotClient.SendAsync(
                HttpMethod.Post,
                "/v1.1/webhook/setupWebhook",
                new SwitchBotRequest()
                {
                    Body = new Dictionary<string, object>()
                    {
                        ["action"] = "setupWebhook",
                        ["url"] = url,
                        ["deviceList"] = deviceList
                    }
                },
                cancellationToken
            )
            .ConfigureAwait(false);
    }

    public async Task DeleteWebhookAsync(Uri url, CancellationToken cancellationToken = default)
    {
        _ = await this
            .switchBotClient.SendAsync(
                HttpMethod.Post,
                "/v1.1/webhook/deleteWebhook",
                new SwitchBotRequest()
                {
                    Body = new Dictionary<string, object>()
                    {
                        ["action"] = "deleteWebhook",
                        ["url"] = url
                    }
                },
                cancellationToken
            )
            .ConfigureAwait(false);
    }

    public async Task<WebhookDetails[]> GetWebhookDetailsAsync(Uri[] urls, CancellationToken cancellationToken = default)
    {
        var response = await this
            .switchBotClient.SendAsync(
                HttpMethod.Post,
                "/v1.1/webhook/queryWebhook",
                new SwitchBotRequest()
                {
                    Body = new Dictionary<string, object>()
                    {
                        ["action"] = "queryDetails",
                        ["urls"] = urls
                    }
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        return response.Body.Deserialize<WebhookDetails[]>() ?? [];
    }

    public async Task<Uri[]> GetWebhookUrlsAsync(CancellationToken cancellationToken = default)
    {
        var response = await this
            .switchBotClient.SendAsync(
                HttpMethod.Post,
                "/v1.1/webhook/queryWebhook",
                new SwitchBotRequest()
                {
                    Body = new Dictionary<string, object>()
                    {
                        ["action"] = "queryUrl"
                    }
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        return response
                   .Body?["urls"]
                   ?.Deserialize<Uri[]>() ??
               [];
    }

}
