//
// Copyright (c) 2024-2025 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot/blob/main/LICENSE
//

using Aspire.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

var builder = DistributedApplication.CreateBuilder(args);

var storage = builder
    .AddAzureStorage("storage")
    .RunAsEmulator();
var blobs = storage.AddBlobs("blobs");
var container = blobs.AddBlobContainer("container", "switchbot-event-data");

_ = builder
    .AddProject<Projects.Karamem0_SwitchBot_Web>("server")
    .WithReference(container)
    .WaitFor(container);

var app = builder.Build();

await app.RunAsync();
