using FortuneCookie.Core.Responses;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FortuneCookie.Bot;

public class TelegramHelper
{
    private const string _startCommand = "/start";
    public async Task ErrorHandler(ITelegramBotClient client, Exception update, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateHandler(ITelegramBotClient client, Update update, CancellationToken cancellationToken)
    {

        if (!(update.Type == UpdateType.Message && update.Message!.Type == MessageType.Text)) return;
        var id = update!.Message!.Chat.Id;
        var text = update!.Message!.Text;
        var userName = update.Message.Chat.FirstName;

        if (Equals(text, _startCommand))
        {
            //welcome command
        }
        else
        {
            await client.SendTextMessageAsync(id, BotResponse.GetDefaultResponse());
        }
    }
}