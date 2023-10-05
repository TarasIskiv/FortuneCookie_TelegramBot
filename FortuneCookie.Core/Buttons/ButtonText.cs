namespace FortuneCookie.Core.Buttons;

public static class ButtonText
{
    public static string GetDailyPredictionButton(int currentCount, int maxCount) => $"Get Daily Prediction ({currentCount} / {maxCount})";

    public static string EnableDisableNotificationsButton(bool isNotificationsAllowed)
                                                    => isNotificationsAllowed ? "Turn off notifications" : "Turn on notifications";
}