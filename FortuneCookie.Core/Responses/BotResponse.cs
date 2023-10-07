namespace FortuneCookie.Core.Responses;

public static class BotResponse
{
    public static string GetDefaultResponse()
    {
        return @"I cant recognize your message. Use buttons to communicate with me";
    }

    public static string NewUserResponse()
    {
        return @"Hi, I'm a Fortune Cookie bot. I'll send you a prediction every day at 9:00 am";
    }

    public static string NotificationStatusChangedResponse(bool isNotificationsAllowed)
    {
        var notificationStatus = isNotificationsAllowed ? "on" : "off";
        return $"You turned {notificationStatus} your notifications";
    }

    public static string DailyPredictionResponse(string username, string prediction)
    {
        return $"{username}, here's your daily prediction.\n{prediction}";
    }
    
}