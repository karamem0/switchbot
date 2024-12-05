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

public record CreateEventDataRequest
{

    [JsonPropertyName("eventType")]
    public string? EventType { get; set; }

    [JsonPropertyName("eventVersion")]
    public string? EventVersion { get; set; }

    [JsonPropertyName("deviceType")]
    public string? DeviceType { get; set; }

    [JsonPropertyName("deviceMac")]
    public string? DeviceMac { get; set; }

    [JsonPropertyName("temperature")]
    public float Temperature { get; set; }

    [JsonPropertyName("scale")]
    public string? Scale { get; set; }

    [JsonPropertyName("humidity")]
    public float Humidity { get; set; }

    [JsonPropertyName("CO2")]
    public int CO2 { get; set; }

    [JsonPropertyName("battery")]
    public int Battery { get; set; }

    [JsonPropertyName("timeOfSample")]
    public DateTimeOffset TimeOfSample { get; set; }

}
