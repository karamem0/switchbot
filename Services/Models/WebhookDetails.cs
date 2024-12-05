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

namespace Karamem0.SwitchBot.Services.Models;

public record WebhookDetails
{

    [JsonPropertyName("url")]
    public required string Url { get; set; }

    [JsonPropertyName("createTime")]
    public required long CreateTime { get; set; }

    [JsonPropertyName("lastUpdateTime")]
    public required long LastUpdateTime { get; set; }

    [JsonPropertyName("deviceList")]
    public required string DeviceList { get; set; }

    [JsonPropertyName("enable")]
    public required bool Enable { get; set; }

}
