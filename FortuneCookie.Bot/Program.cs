﻿using System.Globalization;
using FortuneCookie.Bot;
using FortuneCookie.Bot.DependencyInjection;
using FortuneCookie.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

var serviceProvider = new ServiceCollection();
serviceProvider.AddDependencyInjections();
    

//var client = new TelegramBotClient("5963275949:AAGV74NP8r9vk2nqFXCE7XQsJGqyN0PQqrA");
//
// var receiverOptions = new ReceiverOptions()
// {
//     AllowedUpdates = new UpdateType[]
//     {
//         UpdateType.Message,
//         UpdateType.EditedMessage
//     }
// };
//
// var helper = new TelegramHelper();
//
// client.StartReceiving(helper.UpdateHandler, helper.ErrorHandler, receiverOptions);
//
// Console.ReadLine();

