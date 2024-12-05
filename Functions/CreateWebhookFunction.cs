//
// Copyright (c) 2024-2025 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot/blob/main/LICENSE
//

using Karamem0.SwitchBot.Functions.Models;
using Karamem0.SwitchBot.Logging;
using Karamem0.SwitchBot.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Karamem0.SwitchBot.Functions;

public class CreateWebhookFunction(ILoggerFactory loggerFactory, ISwitchBotService switchBotService)
{

    private readonly ILogger logger = loggerFactory.CreateLogger<CreateWebhookFunction>();

    private readonly ISwitchBotService switchBotService = switchBotService;

    [Function("CreateWebhook")]
    public async Task<IActionResult> RunAsync(
        [HttpTrigger(AuthorizationLevel.Function, "POST")][Microsoft.Azure.Functions.Worker.Http.FromBody()] CreateWebhookRequest request,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            this.logger.FunctionExecuting();
            this.logger.FunctionRequestData(requestData: request);
            _ = request.Url ?? throw new InvalidOperationException();
            _ = request.DeviceList ?? throw new InvalidOperationException();
            await this
                .switchBotService.CreateWebhookAsync(
                    request.Url,
                    request.DeviceList,
                    cancellationToken
                )
                .ConfigureAwait(false);
            var response = new CreateWebhookResponse();
            this.logger.FunctionResponseData(responseData: response);
            return new OkObjectResult(response);
        }
        catch (InvalidOperationException ex)
        {
            this.logger.FunctionFailed(exception: ex);
            return new StatusCodeResult(StatusCodes.Status400BadRequest);
        }
        catch (Exception ex)
        {
            this.logger.UnhandledErrorOccurred(exception: ex);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
        finally
        {
            this.logger.FunctionExecuted();
        }
    }

}
