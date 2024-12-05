//
// Copyright (c) 2024-2025 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot/blob/main/LICENSE
//

using Karamem0.SwitchBot;
using Karamem0.SwitchBot.Mappings;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

var builder = FunctionsApplication.CreateBuilder(args);
_ = builder.ConfigureFunctionsWebApplication();

var environmentName = Environment.GetEnvironmentVariable("AZURE_FUNCTIONS_ENVIRONMENT");
var configuration = builder.Configuration;
_ = configuration.AddJsonFile(
    "appsettings.json",
    true,
    true
);
_ = configuration.AddJsonFile(
    $"appsettings.{environmentName}.json",
    true,
    true
);
_ = configuration.AddUserSecrets(typeof(Program).Assembly, true);
_ = configuration.AddEnvironmentVariables();

var services = builder.Services;
_ = services.AddApplicationInsightsTelemetryWorkerService();
_ = services.ConfigureFunctionsApplicationInsights();
_ = services.AddHttpClient(configuration);
_ = services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
_ = services.AddServices(configuration);

var app = builder.Build();

await app.RunAsync();
