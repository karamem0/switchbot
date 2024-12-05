//
// Copyright (c) 2024 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot/blob/main/LICENSE
//

using Karamem0.SwitchBot;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

var builder = new HostBuilder();
_ = builder.ConfigureFunctionsWorkerDefaults();
_ = builder.ConfigureAppConfiguration((context, builder) =>
{
    _ = builder.AddJsonFile("appsettings.json", true, true);
    _ = builder.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("AZURE_FUNCTIONS_ENVIRONMENT")}.json", true, true);
    _ = builder.AddUserSecrets(typeof(Program).Assembly, true);
    _ = builder.AddEnvironmentVariables();
});
_ = builder.ConfigureServices((context, services) =>
{
    _ = services.AddApplicationInsightsTelemetryWorkerService(options =>
    {
        options.EnableAdaptiveSampling = false;
        options.EnableQuickPulseMetricStream = false;
    });
    _ = services.AddLogging(builder => builder.AddApplicationInsights());
    _ = services.AddServices(context.Configuration);
});

var app = builder.Build();

await app.RunAsync();
