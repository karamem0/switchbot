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
using System.Threading;
using System.Threading.Tasks;

namespace Karamem0.SwitchBot.Services;

public interface ISwitchBotService
{

    Task<CreateWebhookResponse> CreateWebhookAsync(
        string url,
        string deviceList,
        CancellationToken cancellationToken = default
    );

    Task<DeleteWebhookResponse> DeleteWebhookAsync(string url, CancellationToken cancellationToken = default);

    Task<GetWebhookDetailsResponse> GetWebhookDetailsAsync(string[] urls, CancellationToken cancellationToken = default);

    Task<GetWebhookUrlsResponse> GetWebhookUrlsAsync(CancellationToken cancellationToken);

}

public class SwitchBotService(SwitchBotClient switchBotClient) : ISwitchBotService
{

    private readonly SwitchBotClient switchBotClient = switchBotClient;

    public async Task<CreateWebhookResponse> CreateWebhookAsync(
        string url,
        string deviceList,
        CancellationToken cancellationToken = default
    )
    {
        return await this.switchBotClient.SendAsync<CreateWebhookRequest, CreateWebhookResponse>(
            HttpMethod.Post,
            "/v1.1/webhook/setupWebhook",
            new CreateWebhookRequest()
            {
                Url = url,
                DeviceList = deviceList
            },
            cancellationToken
        );
    }

    public async Task<DeleteWebhookResponse> DeleteWebhookAsync(string url, CancellationToken cancellationToken = default)
    {
        return await this.switchBotClient.SendAsync<DeleteWebhookRequest, DeleteWebhookResponse>(
            HttpMethod.Post,
            "/v1.1/webhook/deleteWebhook",
            new DeleteWebhookRequest()
            {
                Url = url
            },
            cancellationToken
        );
    }

    public async Task<GetWebhookDetailsResponse> GetWebhookDetailsAsync(string[] urls, CancellationToken cancellationToken = default)
    {
        return await this.switchBotClient.SendAsync<GetWebhookDetailsRequest, GetWebhookDetailsResponse>(
            HttpMethod.Post,
            "/v1.1/webhook/queryWebhook",
            new GetWebhookDetailsRequest()
            {
                Urls = urls
            },
            cancellationToken
        );
    }

    public async Task<GetWebhookUrlsResponse> GetWebhookUrlsAsync(CancellationToken cancellationToken = default)
    {
        return await this.switchBotClient.SendAsync<GetWebhookUrlsRequest, GetWebhookUrlsResponse>(
            HttpMethod.Post,
            "/v1.1/webhook/queryWebhook",
            new GetWebhookUrlsRequest(),
            cancellationToken
        );
    }

}
