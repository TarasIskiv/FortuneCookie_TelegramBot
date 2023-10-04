using FortuneCookie.Bot;
using FortuneCookie.Bot.DependencyInjection;
using FortuneCookie.Logic.Abstraction;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;

var services = new ServiceCollection();
services.AddDependencyInjections();
using var serviceProvider = services.BuildServiceProvider();

var telegramClient = serviceProvider.GetService<ITelegramBotClient>();
var telegramService = serviceProvider.GetService<ITelegramService>();
var logger = serviceProvider.GetService<ILoggerFactory>();

var receiverOptions = new ReceiverOptions() {AllowedUpdates = new UpdateType[]{UpdateType.Message,UpdateType.EditedMessage} };
var helper = new TelegramHelper(telegramService!, logger!);
telegramClient!.StartReceiving(helper.UpdateHandler, helper.ErrorHandler, receiverOptions);

Console.ReadLine();


