using FortuneCookie.Core.Models;

namespace FortuneCookie.Repository.Abstraction;

public interface IUserRepository
{
    Task RegisterUser(UserDetails user);
    Task<bool> CheckIfChatExists(long chatId);
    Task<bool> CheckIfNotificationsAreAllowed(int id);
    Task<UserDetails> GetUser(long chatId);
    Task UpdateDailyPredictionsCount(long chatId);
    Task ChangeNotificationStatus(long id);
    Task RefreshDailyPredictionsCount();
}