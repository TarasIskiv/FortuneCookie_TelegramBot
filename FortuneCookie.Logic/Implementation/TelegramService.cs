using FortuneCookie.Core.Buttons;
using FortuneCookie.Core.Enums;
using FortuneCookie.Core.Models;
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
    private readonly IPredictionService _predictionService;
    private readonly string _startCommand = "/start";
    private MessageInfo _message;
    private UserDetails _currentUser;
    public TelegramService(IUserService userService, ITelegramBotClient client, IPredictionService predictionService)
    {
        _userService = userService;
        _client = client;
        _predictionService = predictionService;
        _message = new();
        _currentUser = new();
    }
    public async Task SendMessage(ResponseMessageType messageType)
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
            case ResponseMessageType.NotificationStatus:
                await _userService.ChangeNotificationStatus(_currentUser.ChatId);
                _currentUser = await _userService.GetUser(_currentUser.ChatId);
                message = BotResponse.NotificationStatusChangedResponse(_currentUser.IsNotificationAllowed);
                break;
            case ResponseMessageType.ManualDailyPrediction:
                message = await _predictionService.GetPrediction();
                await _userService.UpdateDailyPredictionsCount(_currentUser.ChatId);
                _currentUser = await _userService.GetUser(_currentUser.ChatId);
                break;
        }

        var predictionsAreBlocked = _currentUser.MaxDailyPredictionsCount == _currentUser.CurrentDailyPredictionsCount;
        var buttons = GetButtons(predictionsAreBlocked);
        await _client.SendTextMessageAsync(_message.ChatId, message, replyMarkup: buttons);
    }

    public async Task ReceiveMessage(Update update)
    {
        if (!(update.Type == UpdateType.Message && update.Message!.Type == MessageType.Text)) return;
        SetMessageInfo(update);
        
        var messageType = await GetMessageType();;
        if (Equals(_message.Text, _startCommand))
        {
            messageType = ResponseMessageType.NewUser;
            await _userService.RegisterUser(_message.ChatId, _message.Username);
        }

        _currentUser = await _userService.GetUser(_message.ChatId);
        await SendMessage(messageType);
    }

    private IReplyMarkup GetButtons(bool isPredictionsDisabled)
    {
        var buttons = new ReplyKeyboardMarkup(new List<KeyboardButton>());
        buttons.Keyboard = isPredictionsDisabled ? GetOnlyNotificationsKeyboard() : GetDefaultKeyboard();
        return buttons;
    }

    private KeyboardButton[][] GetDefaultKeyboard()
    {
        return new KeyboardButton[][]
        {
            new KeyboardButton[]
            {
                new KeyboardButton(ButtonText.GetDailyPredictionButton(_currentUser.CurrentDailyPredictionsCount,
                    _currentUser.MaxDailyPredictionsCount)),
                new KeyboardButton(ButtonText.EnableDisableNotificationsButton(_currentUser.IsNotificationAllowed))
            }
        };
    }

    private KeyboardButton[][] GetOnlyNotificationsKeyboard()
    {
        return new KeyboardButton[][]
        {
            new KeyboardButton[]
            {
                new KeyboardButton(ButtonText.EnableDisableNotificationsButton(_currentUser.IsNotificationAllowed))
            }
        };
    }

    private bool IsButtonClicked(UserDetails user)
    {
        return Equals(_message.Text,
                   ButtonText.GetDailyPredictionButton(user.CurrentDailyPredictionsCount,
                       user.MaxDailyPredictionsCount))
               || Equals(_message.Text, ButtonText.EnableDisableNotificationsButton(user.IsNotificationAllowed));
    }

    private async Task<ResponseMessageType> GetMessageType()
    {
        var user = await _userService.GetUser(_message.ChatId);
        if (user is null) return ResponseMessageType.Default;
        var isButton = IsButtonClicked(user);
        if (!isButton) return ResponseMessageType.Default;

        return Equals(_message.Text, ButtonText.EnableDisableNotificationsButton(user.IsNotificationAllowed))
            ? ResponseMessageType.NotificationStatus
            : ResponseMessageType.ManualDailyPrediction;
    }

    private void SetMessageInfo(Update update)
    {
        _message = new MessageInfo()
        {
            ChatId = update!.Message!.Chat.Id,
            Text = update!.Message!.Text ?? default!,
            Username = update.Message.Chat.FirstName ?? default!
        };
    }
}