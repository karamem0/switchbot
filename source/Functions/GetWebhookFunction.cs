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
        [HttpTrigger(AuthorizationLevel.Function, "POST")][Microsoft.Azure.Functions.Worker.Http.FromBody()] GetWebhookRequest _,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            this.logger.FunctionExecuting();
            var response1 = await this.switchBotService.GetWebhookUrlsAsync(cancellationToken);
            if (response1?.Body?.Urls is null)
            {
                return new OkObjectResult(new GetWebhookResponse());
            }
            var response2 = await this.switchBotService.GetWebhookDetailsAsync(response1.Body.Urls, cancellationToken);
            return new OkObjectResult(this.mapper.Map<GetWebhookResponse>(response2));
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
                .CreateMap<ServiceModels.GetWebhookDetailsResponse, GetWebhookResponse>()
                .ForMember(d => d.Value, o => o.MapFrom(s => s.Body));
            _ = this
                .CreateMap<ServiceModels.GetWebhookDetailsResponseBody, GetWebhookResponseValue>()
                .ForMember(d => d.CreateTime, o => o.MapFrom(s => DateTimeOffset.FromUnixTimeMilliseconds(s.CreateTime)))
                .ForMember(d => d.LastUpdateTime, o => o.MapFrom(s => DateTimeOffset.FromUnixTimeMilliseconds(s.LastUpdateTime)));
        }

    }

}
