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
}