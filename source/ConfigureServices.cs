//
// Copyright (c) 2024-2025 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot/blob/main/LICENSE
//

using Azure.Data.Tables;
using Azure.Identity;
using Karamem0.SwitchBot.Clients;
using Karamem0.SwitchBot.Options;
using Karamem0.SwitchBot.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.SwitchBot;

public static class ConfigureServices
{

    public static IServiceCollection AddHttpClient(this IServiceCollection services, IConfiguration configuration)
    {
        _ = services.AddHttpClient(
            "SwitchBot",
            client =>
            {
                var options = configuration
                    .GetSection("SwitchBot")
                    .Get<SwitchBotOptions>();
                _ = options ?? throw new InvalidOperationException();
                client.BaseAddress = options.Endpoint ?? throw new InvalidOperationException();
            }
        );
        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        _ = services.AddSingleton(provider =>
            {
                var options = configuration
                    .GetSection("AzureDataTables")
                    .Get<AzureDataTablesOptions>();
                _ = options ?? throw new InvalidOperationException();
                return new TableServiceClient(
                    options.Endpoint,
                    new DefaultAzureCredential(
                        new DefaultAzureCredentialOptions()
                        {
                            ManagedIdentityClientId = options.ClientId
                        }
                    )
                );
            }
        );
        _ = services.AddSingleton(provider =>
            {
                var options = configuration
                    .GetSection("SwitchBot")
                    .Get<SwitchBotOptions>();
                _ = options ?? throw new InvalidOperationException();
                return new SwitchBotClient(
                    provider.GetRequiredService<ILoggerFactory>(),
                    provider.GetRequiredService<IHttpClientFactory>(),
                    options.AccessToken ?? throw new InvalidOperationException(),
                    options.ClientSecret ?? throw new InvalidOperationException()
                );
            }
        );
        _ = services.AddSingleton<ITableService, TableService>();
        _ = services.AddSingleton<ISwitchBotService, SwitchBotService>();
        return services;
    }

}
