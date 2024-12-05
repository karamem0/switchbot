//
// Copyright (c) 2024 karamem0
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

namespace Karamem0.SwitchBot.Models;

public record EventData
{

    [JsonPropertyName("eventType")]
    public required string EventType { get; init; }

    [JsonPropertyName("eventVersion")]
    public required string EventVersion { get; init; }

    [JsonPropertyName("context")]
    public required EventContextData Context { get; init; }

}
