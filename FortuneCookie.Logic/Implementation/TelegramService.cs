using FortuneCookie.Core.Buttons;
using FortuneCookie.Core.Enums;
using FortuneCookie.Core.Responses;
using FortuneCookie.Logic.Abstraction;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace FortuneCookie.Logic.Implementation;

public class TelegramService : ITelegramService
{
    private readonly IUserService _userService;
    private readonly ITelegramBotClient _client;
    private readonly string _startCommand = "/start";

    public TelegramService(IUserService userService, ITelegramBotClient client)
    {
        _userService = userService;
        _client = client;
    }
    public async Task SendMessage(long chatId, ResponseMessageType messageType)
    {
        var message = BotResponse.GetDefaultResponse();
        switch (messageType)
        {
            case ResponseMessageType.Default:
                message = BotResponse.GetDefaultResponse();
                break;
            case ResponseMessageType.NewUser:
                message = BotResponse.NewUserResponse();
                break;
            case ResponseMessageType.DailyPrediction:
                break;
            case ResponseMessageType.ManualDailyPrediction:
                break;
        }

        var buttons = await GetButtons(chatId);
        await _client.SendTextMessageAsync(chatId, message, replyMarkup: buttons);
    }

    public async Task ReceiveMessage(Update update)
    {
        if (!(update.Type == UpdateType.Message && update.Message!.Type == MessageType.Text)) return;
        var id = update!.Message!.Chat.Id;
        var text = update!.Message!.Text;
        var userName = update.Message.Chat.FirstName;

        var messageType = ResponseMessageType.Default;
        if (Equals(text, _startCommand))
        {
            messageType = ResponseMessageType.NewUser;
            await _userService.RegisterUser(id, userName!);
        } 
        await SendMessage(id, messageType);
    }

    private async Task<IReplyMarkup> GetButtons(long chatId)
    {
        var user = await _userService.GetUser(chatId);
        var buttons = new ReplyKeyboardMarkup(new List<KeyboardButton>());
        buttons.Keyboard = new KeyboardButton[][]  
        {  
            new KeyboardButton[]  
            {
                new KeyboardButton(ButtonText.GetDailyPredictionButton(user.CurrentDailyPredictionsCount, user.MaxDailyPredictionsCount)),
                new KeyboardButton(ButtonText.EnableDisableNotificationsButton(user.IsNotificationAllowed))
            }
        };
        return buttons;
    }
}