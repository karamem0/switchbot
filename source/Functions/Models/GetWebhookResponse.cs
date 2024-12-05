//
// Copyright (c) 2024-2025 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot/blob/main/LICENSE
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Karamem0.SwitchBot.Functions.Models;

public record GetWebhookResponse
{

    [JsonPropertyName("value")]
    public GetWebhookResponseValue[]? Value { get; set; } = [];

}

public record GetWebhookResponseValue
{

    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("createTime")]
    public DateTimeOffset CreateTime { get; set; }

    [JsonPropertyName("lastUpdateTime")]
    public DateTimeOffset LastUpdateTime { get; set; }

    [JsonPropertyName("deviceList")]
    public string? DeviceList { get; set; }

    [JsonPropertyName("enable")]
    public bool Enable { get; set; }

}
