# FortuneCookie_TelegramBot
# This is the telegram bot, that sends predictions to the users.
This bot includes 2 Azure functions:
- Refresh the current daily predictions count value
- Send daily predictions to the users, who have notifications are allowed

As a user, you can:
- Get 3 predictions per day.
- Turn on/off notifications, if you don't want to receive the prediction, that is sent each day in the morning.

# How to start? (FortuneCookie.Bot project)
1. Pull the repository
2. Install Postgresql, .Net 7.0, if you haven't installed them
3. Run Postgresql
4. Create a Database and table in Postgresql
5. Go to the BotFather bot in Telegram and generate a new token for the bot.
6. Copy and paste this token into the appsettings file in FortuneCookie.Bot
7. Don't forget to update the connection string for the database
8. Go to the FortuneCookie.Bot folder and run ```dotnet build```
9. After the build is finished successfully, run the application using the command ```dotnet run```.
10. Now, your bot is working and you can interact with it.
12. Enjoy!

# How to start? (FortuneCookie.Functions project)
1. Install Azurite, if you haven't done it
2. Run Azurite
3. Update the token in the appsettings.json file in the FortuneCookie.Functions project
4. These functions are timer-triggered functions, but if you want to start them manually, you need to update the TimerTrigger parameter in the functions
5. ```[TimerTrigger("0 5 0 * * *", RunOnStartup = true)]``` Add RunOnStartup=true
6. Enjoy!

# Technologies

<img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/csharp/csharp-original.svg" alt="csharp" width="40" height="40"/> </a> <a href="https://learn.microsoft.com/pl-pl/dotnet/" target="_blank" rel="noreferrer">
<img src="https://i.pinimg.com/originals/06/86/c0/0686c0c85407548ea5bd737a572974b6.png" alt="postgresql" width="60" height="60"/> </a> <a href="https://www.postgresql.org/" target="_blank" rel="noreferrer">
<img src="https://i0.wp.com/mattruma.com/wp-content/uploads/2020/04/AzureFunctions.png" alt="azureFunctions" width="60" height="60"/> </a> <a href="https://azure.microsoft.com/en-us/products/functions/" target="_blank" rel="noreferrer">


