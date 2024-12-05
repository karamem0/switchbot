//
// Copyright (c) 2024-2025 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot/blob/main/LICENSE
//

using AutoMapper;
using Karamem0.SwitchBot.Controllers.Models;
using Karamem0.SwitchBot.Logging;
using Karamem0.SwitchBot.Services;
using Karamem0.SwitchBot.Services.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Karamem0.Teamtile.Controllers;

[ApiController()]
[Route("api/executeWebhook")]
public class ExecuteWebHookController(
    IBlobsService blobsService,
    IMapper mapper,
    ILogger<ExecuteWebHookController> logger
) : Controller
{

    private readonly IBlobsService blobsService = blobsService;

    private readonly IMapper mapper = mapper;

    private readonly ILogger logger = logger;

    public async Task<IActionResult> PostAsync([FromBody] ExecuteWebhookRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            this.logger.FunctionExecuting();
            this.logger.FunctionRequestData(requestData: request);
            await this
                .blobsService.CreateEventDataAsync(this.mapper.Map<EventData>(request.Context), cancellationToken)
                .ConfigureAwait(false);
            var response = new ExecuteWebhookResponse();
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

    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile()
        {
            _ = this
                .CreateMap<ExecuteWebhookRequestContext, EventData>()
                .ForMember(d => d.TimeOfSample, o => o.MapFrom(s => DateTimeOffset.FromUnixTimeMilliseconds(s.TimeOfSample)));
        }

    }

}
