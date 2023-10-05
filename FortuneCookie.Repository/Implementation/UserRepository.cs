using FortuneCookie.Core.Models;
using FortuneCookie.Database;
using FortuneCookie.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace FortuneCookie.Repository.Implementation;

public class UserRepository : IUserRepository
{
    private readonly FortuneCookieContext _context;

    public UserRepository(FortuneCookieContext context)
    {
        _context = context;
    }
    public async Task RegisterUser(UserDetails user)
    {
        await _context.UsersDetails.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> CheckIfChatExists(long chatId)
    {
        return await Task.Run(() => _context.UsersDetails.Any(user => user.ChatId == chatId));
    }

    public async Task<bool> CheckIfNotificationsAreAllowed(int id)
    {
        var userDetails = await Task.Run(() => _context.UsersDetails.FirstOrDefault(user => user.Id == id));
        return userDetails!.IsNotificationAllowed;
    }

    public async Task<UserDetails> GetUser(long chatId)
    {
        return await Task.Run(() => _context.UsersDetails.FirstOrDefault(user => user.ChatId == chatId)) ?? default!;
    }

    public async Task UpdateDailyPredictionsCount(long chatId)
    {
        var user = await GetUser(chatId);
        if (user.CurrentDailyPredictionsCount == user.MaxDailyPredictionsCount) return;
        user.CurrentDailyPredictionsCount += 1;
        _context.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task ChangeNotificationStatus(long id)
    {
        var user = await GetUser(id);
        user.IsNotificationAllowed = !user.IsNotificationAllowed;
        _context.Update(user);
        await _context.SaveChangesAsync();
    }
}