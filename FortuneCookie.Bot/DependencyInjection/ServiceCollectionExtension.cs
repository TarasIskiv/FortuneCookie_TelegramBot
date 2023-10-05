using FortuneCookie.Core.Models;
using FortuneCookie.Database;
using FortuneCookie.Logic.Abstraction;
using FortuneCookie.Logic.Implementation;
using FortuneCookie.Repository.Abstraction;
using FortuneCookie.Repository.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;

namespace FortuneCookie.Bot.DependencyInjection;

public static class ServiceCollectionExtension
{
    public static void AddDependencyInjections(this ServiceCollection services)
    {
        var config = GetConfiguration();
        var databaseConnection = config.GetSection("ConnectionStrings")?.GetSection("Database");
        var token = config.GetSection("ConnectionStrings")?.GetSection("TelegramBotKey")?.Get<string>() ?? string.Empty;
        services
            .AddLogging()
            .AddDbContext<FortuneCookieContext>(options => options.UseNpgsql(databaseConnection?.Get<string>()))
            .AddSingleton<ITelegramBotClient>(client => new TelegramBotClient(token))
            .AddScoped<IUserService, UserService>()
            .AddTransient<ITelegramService, TelegramService>()
            .AddScoped<IUserRepository, UserRepository>();
    }
    private static IConfiguration GetConfiguration()
    {
        var builder = new ConfigurationBuilder();
        builder.InitializeBuilder();
        return builder.Build();
    }

    private static void InitializeBuilder(this ConfigurationBuilder builder)
    {
        builder.AddJsonFile("appsettings.json", optional: false);
    }
    
     
}