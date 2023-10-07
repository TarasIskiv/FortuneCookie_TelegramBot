using System;
using FortuneCookie.Core.Enums;
using FortuneCookie.Functions.Helpers;
using FortuneCookie.Logic.Abstraction;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using TimerInfo = Microsoft.Azure.Functions.Worker.TimerInfo;

namespace FortuneCookie.Functions;

public class DailyFunctions : FunctionBase<DailyFunctions>
{
    public DailyFunctions(IUserService userService, ITelegramService telegramService, ILoggerFactory loggerFactory) : base(telegramService, userService, loggerFactory)
    {
    }
    
    [Function("RefreshDailyPredictionsCount")]
    public async Task Run([TimerTrigger("0 5 0 * * *")] TimerInfo myTimer)
    {
        try
        {
            await _userService.RefreshDailyPredictionsCount();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
    }
    
    [Function("SendDailyPrediction")]
    public async Task RunFunction([TimerTrigger("0 0 8 * * *")] TimerInfo myTimer)
    {
        try
        {
            await _telegramService.SendMessage(ResponseMessageType.DailyPrediction);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
    }
}
