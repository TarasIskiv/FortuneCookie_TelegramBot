using FortuneCookie.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace FortuneCookie.Database;

public class FortuneCookieContext : DbContext
{
    public FortuneCookieContext(DbContextOptions options) : base(options)
    {
        
    }
    
    public DbSet<UserDetails> UsersDetails { get; set; }
}