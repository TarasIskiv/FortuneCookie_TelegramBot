using FortuneCookie.Database;
using FortuneCookie.Functions.Helpers;
using FortuneCookie.Logic.Abstraction;
using FortuneCookie.Logic.Implementation;
using FortuneCookie.Repository.Abstraction;
using FortuneCookie.Repository.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureAppConfiguration((context, builder) =>
    {
        builder.AddJsonFile("local.settings.json", optional: true);
    })
    .ConfigureServices((context, services) =>
    {
        var databaseConnection = context.Configuration.GetSection("ConnectionStrings")?.GetSection("Database");
        var token = context.Configuration.GetSection("ConnectionStrings")?.GetSection("TelegramBotKey")?.Get<string>() ?? string.Empty;
        services.AddScoped<IUserService, UserService>()
            .AddSingleton<ITelegramBotClient>(client => new TelegramBotClient(token))
            .AddScoped<IUserRepository, UserRepository>()
            .AddTransient<ITelegramService, TelegramService>()
            .AddDbContext<FortuneCookieContext>(options => options.UseNpgsql(databaseConnection?.Get<string>()));
    })
    .Build();

host.Run();