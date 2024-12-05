//
// Copyright (c) 2024-2025 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot/blob/main/LICENSE
//

using Azure.Data.Tables;
using Karamem0.SwitchBot.Options;
using Karamem0.SwitchBot.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Karamem0.SwitchBot.Services;

public interface ITableService
{

    Task<CreateEventDataResponse> CreateEventDataAsync(CreateEventDataRequest value, CancellationToken cancellationToken);

}

public class TableService(TableServiceClient tableServiceClient, AzureDataTablesOptions serviceOptions) : ITableService
{

    private readonly TableServiceClient tableServiceClient = tableServiceClient;

    private readonly string tableName = serviceOptions.TableName ?? throw new InvalidOperationException();

    public async Task<CreateEventDataResponse> CreateEventDataAsync(CreateEventDataRequest value, CancellationToken cancellationToken = default)
    {
        _ = await this.tableServiceClient.CreateTableIfNotExistsAsync(this.tableName, cancellationToken: cancellationToken);
        var tableClient = this.tableServiceClient.GetTableClient(this.tableName);
        var tableEntry = new TableEntity(value.DeviceMac, Guid.NewGuid().ToString())
        {
            ["EventType"] = value.EventType,
            ["EventVersion"] = value.EventVersion,
            ["DeviceType"] = value.DeviceType,
            ["DeviceMac"] = value.DeviceMac,
            ["Temperature"] = value.Temperature,
            ["Scale"] = value.Scale,
            ["Humidity"] = value.Humidity,
            ["CO2"] = value.CO2,
            ["Battery"] = value.Battery,
            ["TimeOfSample"] = value.TimeOfSample
        };
        _ = await tableClient.AddEntityAsync(tableEntry, cancellationToken: cancellationToken);
        return new CreateEventDataResponse()
        {
            PartitionKey = tableEntry.PartitionKey,
            RowKey = tableEntry.RowKey
        };
    }

}
