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

public record EventData
{

    [JsonPropertyName("deviceType")]
    public required string DeviceType { get; set; }

    [JsonPropertyName("deviceMac")]
    public required string DeviceMac { get; set; }

    [JsonPropertyName("temperature")]
    public required float Temperature { get; set; }

    [JsonPropertyName("scale")]
    public required string Scale { get; set; }

    [JsonPropertyName("humidity")]
    public required float Humidity { get; set; }

    [JsonPropertyName("CO2")]
    public required int CO2 { get; set; }

    [JsonPropertyName("battery")]
    public required int Battery { get; set; }

    [JsonPropertyName("timeOfSample")]
    public required DateTimeOffset TimeOfSample { get; set; }

}
