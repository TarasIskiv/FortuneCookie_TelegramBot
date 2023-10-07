using System.Reflection;
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
        builder.AddJsonFile("appsettings.json", optional: false);
    })
    .ConfigureServices((context, services) =>
    {
        var databaseConnection = context.Configuration.GetSection("ConnectionStrings")?.GetSection("Database");
        var token = context.Configuration.GetSection("ConnectionStrings")?.GetSection("TelegramBotKey")?.Get<string>() ?? string.Empty;
        services
            .AddLogging()
            .AddScoped<IUserService, UserService>()
            .AddSingleton<ITelegramBotClient>(client => new TelegramBotClient(token))
            .AddScoped<IUserRepository, UserRepository>()
            .AddTransient<ITelegramService, TelegramService>()
            .AddDbContext<FortuneCookieContext>(options => options.UseNpgsql(databaseConnection?.Get<string>()))
            .AddHttpClient<IPredictionService, PredictionService>(client => { client.BaseAddress = new Uri("https://api.forismatic.com/api/1.0/"); });
    })
    .UseDefaultServiceProvider(options => options.ValidateScopes = false)
    .Build();

host.Run();