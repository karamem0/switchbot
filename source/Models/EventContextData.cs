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

public record EventContextData
{

    [JsonPropertyName("deviceType")]
    public required string DeviceType { get; init; }

    [JsonPropertyName("deviceMac")]
    public required string DeviceMac { get; init; }

    [JsonPropertyName("temperature")]
    public required float Temperature { get; init; }

    [JsonPropertyName("scale")]
    public required string Scale { get; init; }

    [JsonPropertyName("humidity")]
    public required float Humidity { get; init; }

    [JsonPropertyName("CO2")]
    public required int CO2 { get; init; }

    [JsonPropertyName("battery")]
    public required int Battery { get; init; }

    [JsonPropertyName("timeOfSample")]
    public required long TimeOfSample { get; init; }

}
