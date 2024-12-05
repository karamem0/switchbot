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
using Karamem0.SwitchBot.Services.Models;
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

public class GetWebhookFunction(
    ILoggerFactory loggerFactory,
    IMapper mapper,
    ISwitchBotService switchBotService
)
{

    private readonly ILogger logger = loggerFactory.CreateLogger<GetWebhookFunction>();

    private readonly IMapper mapper = mapper;

    private readonly ISwitchBotService switchBotService = switchBotService;

    [Function("GetWebhook")]
    public async Task<IActionResult> RunAsync(
        [HttpTrigger(AuthorizationLevel.Function, "POST")][Microsoft.Azure.Functions.Worker.Http.FromBody()] GetWebhookRequest request,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            this.logger.FunctionExecuting();
            this.logger.FunctionRequestData(requestData: request);
            var webhookUrls = await this
                .switchBotService.GetWebhookUrlsAsync(cancellationToken)
                .ConfigureAwait(false);
            var webookDetails = await this
                .switchBotService.GetWebhookDetailsAsync(webhookUrls, cancellationToken)
                .ConfigureAwait(false);
            var response = new GetWebhookResponse()
            {
                Value = this.mapper.Map<GetWebhookResponseValue[]>(webookDetails)
            };
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
                .CreateMap<WebhookDetails, GetWebhookResponseValue>()
                .ForMember(d => d.CreateTime, o => o.MapFrom(s => DateTimeOffset.FromUnixTimeMilliseconds(s.CreateTime)))
                .ForMember(d => d.LastUpdateTime, o => o.MapFrom(s => DateTimeOffset.FromUnixTimeMilliseconds(s.LastUpdateTime)));
        }

    }

}
