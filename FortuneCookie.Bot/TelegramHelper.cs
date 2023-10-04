using FortuneCookie.Core.Responses;
using FortuneCookie.Logic.Abstraction;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FortuneCookie.Bot;

public class TelegramHelper
{
    private readonly ITelegramService _telegramService;
    private readonly ILogger _logger;

    public TelegramHelper(ITelegramService telegramService, ILoggerFactory logger)
    {
        _telegramService = telegramService;
        _logger = logger.CreateLogger<Program>();
    }
    public async Task ErrorHandler(ITelegramBotClient client, Exception exception, CancellationToken cancellationToken)
    {
        await Task.Run(() => _logger.LogError(exception.Message));
    }

    public async Task UpdateHandler(ITelegramBotClient client, Update update, CancellationToken cancellationToken)
    {
        await _telegramService.ReceiveMessage(update);
    }
}