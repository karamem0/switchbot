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

namespace Karamem0.SwitchBot.Clients;

public record SwitchBotRequest()
{

    public IDictionary<string, object>? Body { get; set; } = new Dictionary<string, object>();

}
