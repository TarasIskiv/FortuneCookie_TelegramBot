using FortuneCookie.Core.Models;

namespace FortuneCookie.Logic.Abstraction;

public interface IUserService
{
    Task RegisterUser(long chatId, string username);
    Task<bool> CheckIfNotificationsAreAllowed(int id);
    Task<UserDetails> GetUser(long chatId);
    Task UpdateDailyPredictionsCount(long chatId);
}