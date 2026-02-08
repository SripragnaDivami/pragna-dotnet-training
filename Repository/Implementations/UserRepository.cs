using capstone_policy_management.Data;
using capstone_policy_management.Entities;
using capstone_policy_management.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace capstone_policy_management.Repository.Implementations;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext context;

    public UserRepository(ApplicationDbContext _context)
    {
        context = _context;
    }

    public async Task<User> CreateUserAsync(User user)
    {
        context.Users.Add(user);
        await context.SaveChangesAsync();
        return user;
    }

    public async Task<List<User>> GetAllUsersAsync()
    { 
        return await context.Users.ToListAsync();
        
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    
    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        context.Users.Update(user);
        await context.SaveChangesAsync();
        return user;
    }

    public async Task DeleteUserAsync(int id)
    {
        var user = await context.Users.FindAsync(id);
        if (user != null)
        {
            context.Users.Remove(user);
            await context.SaveChangesAsync();
        }
    }
}