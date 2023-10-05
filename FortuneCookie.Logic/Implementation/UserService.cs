using FortuneCookie.Core.Models;
using FortuneCookie.Logic.Abstraction;
using FortuneCookie.Repository.Abstraction;

namespace FortuneCookie.Logic.Implementation;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task RegisterUser(long chatId, string username)
    {
        var chatExists = await _userRepository.CheckIfChatExists(chatId);
        if(chatExists) return;
        var user = new UserDetails() { ChatId = chatId, Username = username, CurrentDailyPredictionsCount = 0 };
        await _userRepository.RegisterUser(user);
    }
    
    public async Task<bool> CheckIfNotificationsAreAllowed(int id)
    {
        return await _userRepository.CheckIfNotificationsAreAllowed(id);
    }

    public async Task<UserDetails> GetUser(long chatId)
    {
        return await _userRepository.GetUser(chatId);
    }

    public async Task UpdateDailyPredictionsCount(long chatId)
    {
        await _userRepository.UpdateDailyPredictionsCount(chatId);
    }

    public async Task ChangeNotificationStatus(long id)
    {
        await _userRepository.ChangeNotificationStatus(id);
    }
}