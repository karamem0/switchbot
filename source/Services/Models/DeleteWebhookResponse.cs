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

public record DeleteWebhookResponse : SwitchBotResponse
{

    [JsonPropertyName("body")]
    public DeleteWebhookResponseBody? Body { get; set; }

}

public record DeleteWebhookResponseBody;
