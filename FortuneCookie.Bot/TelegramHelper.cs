using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FortuneCookie.Bot;

public class TelegramHelper
{
    public TelegramHelper()
    {
        
    }
    public async Task ErrorHandler(ITelegramBotClient client, Exception update, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateHandler(ITelegramBotClient client, Update update, CancellationToken cancellationToken)
    {
        
        if (update.Type == UpdateType.Message)
        {
            if (update.Message!.Type == MessageType.Text)
            {
                var text = update.Message.Text;
                var id = update.Message.Chat.Id;
                var userName = update.Message.Chat.FirstName;
                Console.WriteLine($"{text} | {id} | {userName}");
                await client.SendTextMessageAsync(id, "Hello");
            }
        }
    }
}