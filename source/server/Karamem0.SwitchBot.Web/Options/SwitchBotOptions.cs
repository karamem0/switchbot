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
using System.Threading.Tasks;

namespace Karamem0.SwitchBot.Options;

public record SwitchBotOptions
{

    public required Uri Endpoint { get; set; }

    public required string Token { get; set; }

    public required string Secret { get; set; }

}
