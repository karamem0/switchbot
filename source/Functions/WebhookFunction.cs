//
// Copyright (c) 2024 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot/blob/main/LICENSE
//

using Karamem0.SwitchBot.Logging;
using Karamem0.SwitchBot.Models;
using Karamem0.SwitchBot.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.SwitchBot.Functions;

public class WebhookFunction(ILoggerFactory loggerFactory, BlobService blobService)
{

    private readonly ILogger logger = loggerFactory.CreateLogger<WebhookFunction>();

    private readonly BlobService blobService = blobService;

    [Function("Webhook")]
    public async Task<HttpResponseData> RunAsync([HttpTrigger(AuthorizationLevel.Function, "POST")] HttpRequestData req)
    {
        var res = req.CreateResponse();
        try
        {
            this.logger.FunctionStarted();
            var value = await req.ReadFromJsonAsync<EventData>();
            if (value == null)
            {
                this.logger.FunctionFailed();
                res.StatusCode = HttpStatusCode.BadRequest;
                return res;
            }
            await this.blobService.UploadEventDataAsync(value);
            res.StatusCode = HttpStatusCode.OK;
        }
        catch (Exception ex)
        {
            this.logger.UnhandledError(ex);
            res.StatusCode = HttpStatusCode.InternalServerError;
            res.WriteString(ex.Message);
        }
        finally
        {
            this.logger.FunctionEnded();
        }
        return res;
    }

}
