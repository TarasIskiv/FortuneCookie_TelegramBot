namespace FortuneCookie.Core.Models;

public class MessageInfo
{
    public long ChatId { get; set; }
    public string Text { get; set; } = default!;
    public string Username { get; set; } = default!;
}