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

public record CreateWebhookRequest : SwitchBotRequest<CreateWebhookResponse>
{

    [JsonPropertyName("action")]
    public string Action { get; set; } = "setupWebhook";

    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("deviceList")]
    public string? DeviceList { get; set; }

}
