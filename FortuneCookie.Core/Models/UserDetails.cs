using System.ComponentModel.DataAnnotations.Schema;

namespace FortuneCookie.Core.Models;

[Table("UserDetails")]
public class UserDetails
{
    public int Id { get; set; }
    public long ChatId { get; set; }
    public string Username { get; set; } = default!;
    public bool IsNotificationAllowed { get; set; } = true;
    public int CurrentDailyPredictionsCount { get; set; }
    public int MaxDailyPredictionsCount { get; private set; } = 3;
}