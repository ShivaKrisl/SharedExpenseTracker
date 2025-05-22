using ExpenseTracker.Core.DTOs;


namespace ExpenseTracker.Core.Service_Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="userRequest"></param>
        /// <returns></returns>
        public Task<UserResponse> CreateUser(UserRequest userRequest);

        /// <summary>
        /// Gets User by Id.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<UserResponse?> GetUserById(Guid userId);

        /// <summary>
        /// Gets User by Email.
        /// </summary>
        /// <returns></returns>
        public Task<UserResponse?> GetUserByEmail(string userEmail);

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<UserResponse>> GetAllUsers();


    }
}
