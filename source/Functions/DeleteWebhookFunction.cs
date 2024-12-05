//
// Copyright (c) 2024-2025 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot/blob/main/LICENSE
//

using AutoMapper;
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
using ServiceModels = Karamem0.SwitchBot.Services.Models;

namespace Karamem0.SwitchBot.Functions;

public class DeleteWebhookFunction(
    ILoggerFactory loggerFactory,
    IMapper mapper,
    ISwitchBotService switchBotService
)
{

    private readonly ILogger logger = loggerFactory.CreateLogger<DeleteWebhookFunction>();

    private readonly IMapper mapper = mapper;

    private readonly ISwitchBotService switchBotService = switchBotService;

    [Function("DeleteWebhook")]
    public async Task<IActionResult> RunAsync(
        [HttpTrigger(AuthorizationLevel.Function, "POST")][Microsoft.Azure.Functions.Worker.Http.FromBody()] DeleteWebhookRequest request,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            this.logger.FunctionExecuting();
            _ = request.Url ?? throw new InvalidOperationException();
            var response = await this.switchBotService.DeleteWebhookAsync(request.Url, cancellationToken);
            return new OkObjectResult(this.mapper.Map<DeleteWebhookResponse>(response));
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

    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile()
        {
            _ = this.CreateMap<ServiceModels.DeleteWebhookResponse, DeleteWebhookResponse>();
        }

    }

}
