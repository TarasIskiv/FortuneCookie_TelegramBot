using FortuneCookie.Logic.Abstraction;
using Microsoft.Extensions.Logging;

namespace FortuneCookie.Functions.Helpers;

public class FunctionBase<T>
{
    protected readonly ITelegramService _telegramService;
    protected readonly IUserService _userService;
    protected readonly ILogger _logger;
    
    public FunctionBase(ITelegramService telegramService, IUserService userService, ILoggerFactory logger)
    {
        _telegramService = telegramService;
        _userService = userService;
        _logger = logger.CreateLogger<T>();
    }
}