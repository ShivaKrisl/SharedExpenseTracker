using ExpenseTracker.Core.Domain.Entities;
using ExpenseTracker.Core.Domain.Repository_Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Infrastructure.Repository_Classes
{
    public class UsersRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UsersRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Create a User to Db
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<User> CreateUser(User user)
        {
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
            return user;
        }

        /// <summary>
        /// Get all the Users
        /// </summary>
        /// <returns></returns>
        public async Task<List<User>?> GetAllUsers()
        {
            List<User>? users = await _db.Users.ToListAsync();
            return users;
        }

        /// <summary>
        /// Get user by Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<User?> GetUserByEmail(string email)
        {
            User? user = await _db.Users.FirstOrDefaultAsync(e => e.Email.ToLower() == email.ToLower());
            return user;
        }

        /// <summary>
        /// Get user by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<User?> GetUserByUserId(Guid userId)
        {
            User? user = await _db.Users.FirstOrDefaultAsync(e => e.UserId ==  userId);
            return user;
        }
    }
}
