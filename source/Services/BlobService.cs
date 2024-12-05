//
// Copyright (c) 2024 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot/blob/main/LICENSE
//

using Azure.Storage.Blobs;
using Karamem0.SwitchBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.SwitchBot.Services;

public class BlobService(BlobContainerClient blobContainerClient)
{

    private readonly BlobContainerClient blobContainerClient = blobContainerClient;

    public async Task UploadEventDataAsync(EventData value)
    {
        _ = await this.blobContainerClient.CreateIfNotExistsAsync();
        var deviceName = value.Context.DeviceMac;
        var timeOfSample = DateTimeOffset.FromUnixTimeMilliseconds(value.Context.TimeOfSample);
        var blobName = $"{deviceName}/{timeOfSample:yyyy/MM/dd/HHmmss}.json";
        var blobClient = this.blobContainerClient.GetBlobClient(blobName);
        _ = blobClient.UploadAsync(BinaryData.FromObjectAsJson(value));
    }

}
