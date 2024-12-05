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

public class ExecuteWebhookFunction(
    ILoggerFactory loggerFactory,
    IMapper mapper,
    ITableService tableService
)
{

    private readonly ILogger logger = loggerFactory.CreateLogger<ExecuteWebhookFunction>();

    private readonly IMapper mapper = mapper;

    private readonly ITableService tableService = tableService;

    [Function("ExecuteWebhook")]
    public async Task<IActionResult> RunAsync(
        [HttpTrigger(AuthorizationLevel.Function, "POST")][Microsoft.Azure.Functions.Worker.Http.FromBody()] ExecuteWebhookRequest request,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            this.logger.FunctionExecuting();
            _ = await this.tableService.CreateEventDataAsync(
                this.mapper.Map<ExecuteWebhookRequest, ServiceModels.CreateEventDataRequest>(request),
                cancellationToken
            );
            return new OkObjectResult(new ExecuteWebhookResponse());
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
            _ = this
                .CreateMap<ExecuteWebhookRequest, ServiceModels.CreateEventDataRequest>()
                .IncludeMembers(s => s.Context);
            _ = this
                .CreateMap<ExecuteWebhookRequestContext, ServiceModels.CreateEventDataRequest>()
                .ForMember(d => d.TimeOfSample, o => o.MapFrom(s => DateTimeOffset.FromUnixTimeMilliseconds(s.TimeOfSample)));
        }

    }

}
