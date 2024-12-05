//
// Copyright (c) 2024-2025 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot/blob/main/LICENSE
//

using Azure.Storage.Blobs;
using Karamem0.SwitchBot.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Karamem0.SwitchBot.Services;

public interface IBlobsService
{

    Task CreateEventDataAsync(EventData value, CancellationToken cancellationToken);

}

public class BlobsService(BlobContainerClient blobContainerClient) : IBlobsService
{

    private readonly BlobContainerClient blobContainerClient = blobContainerClient;

    public async Task CreateEventDataAsync(EventData value, CancellationToken cancellationToken = default)
    {
        _ = await this.blobContainerClient.CreateIfNotExistsAsync(cancellationToken: cancellationToken);
        _ = await this
            .blobContainerClient.GetBlobClient($"{value.DeviceMac}.json")
            .UploadAsync(
                BinaryData.FromObjectAsJson(value),
                true,
                cancellationToken
            )
            .ConfigureAwait(false);
        _ = await this
            .blobContainerClient.GetBlobClient($"{value.DeviceMac}/{value.TimeOfSample:yyyy/MM/dd/HHmmss}.json")
            .UploadAsync(
                BinaryData.FromObjectAsJson(value),
                true,
                cancellationToken
            )
            .ConfigureAwait(false);
    }

}
