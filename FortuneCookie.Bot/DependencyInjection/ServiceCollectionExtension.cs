using FortuneCookie.Core.Models;
using FortuneCookie.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FortuneCookie.Bot.DependencyInjection;

public static class ServiceCollectionExtension
{
    public static void AddDependencyInjections(this ServiceCollection serviceCollection)
    {
        var config = GetConfiguration();
        var databaseConnection = config.GetSection("ConnectionStrings")?.GetSection("Database");
        serviceCollection
            .AddLogging()
            .AddDbContext<FortuneCookieContext>(options => options.UseNpgsql(databaseConnection?.Get<string>()));

    }

    public static string GetBotToken()
    {
        var config = GetConfiguration();
        return config.GetSection("ConnectionStrings")?.GetSection("TelegramBotKey")?.Get<string>() ?? string.Empty;
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