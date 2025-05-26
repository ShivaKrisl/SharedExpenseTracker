using ExpenseTracker.Core.Domain.Entities;

namespace ExpenseTracker.Core.Domain.Repository_Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Creates an user and save it to DB
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<User> CreateUser(User user);

        /// <summary>
        /// Get User By UserId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<User?> GetUserByUserId(Guid userId);

        /// <summary>
        /// Get User By Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Task<User?> GetUserByEmail(string email);

        /// <summary>
        /// Get All Users
        /// </summary>
        /// <returns></returns>
        public Task<List<User>?> GetAllUsers();
    }
}
