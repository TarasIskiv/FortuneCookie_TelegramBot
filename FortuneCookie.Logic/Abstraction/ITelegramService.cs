using FortuneCookie.Core.Enums;
using Telegram.Bot.Types;

namespace FortuneCookie.Logic.Abstraction;

public interface ITelegramService
{
    Task SendMessage(long chatId, ResponseMessageType messageType);
    Task ReceiveMessage(Update update);
}