//
// Copyright (c) 2024 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot/blob/main/LICENSE
//

using Azure.Identity;
using Azure.Storage.Blobs;
using Karamem0.SwitchBot.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.SwitchBot;

public static class ConfigureServices
{

    private static readonly DefaultAzureCredential defaultAzureCredential = new(new DefaultAzureCredentialOptions()
    {
        ExcludeVisualStudioCodeCredential = true
    });

    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        var blobStorageUrl = configuration["AzureBlobContainerUrl"] ?? throw new InvalidOperationException();
        _ = services.AddSingleton(provider => new BlobContainerClient(new Uri(blobStorageUrl), defaultAzureCredential));
        _ = services.AddSingleton<BlobService>();
        return services;
    }

}
