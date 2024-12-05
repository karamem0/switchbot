//
// Copyright (c) 2024-2025 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot/blob/main/LICENSE
//

using Karamem0.SwitchBot.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Karamem0.SwitchBot.Services.Models;

public record GetWebhookDetailsResponse : SwitchBotResponse
{

    [JsonPropertyName("body")]
    public GetWebhookDetailsResponseBody[]? Body { get; set; }

}

public record GetWebhookDetailsResponseBody
{

    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("createTime")]
    public long CreateTime { get; set; }

    [JsonPropertyName("lastUpdateTime")]
    public long LastUpdateTime { get; set; }

    [JsonPropertyName("deviceList")]
    public string? DeviceList { get; set; }

    [JsonPropertyName("enable")]
    public bool Enable { get; set; }

}
