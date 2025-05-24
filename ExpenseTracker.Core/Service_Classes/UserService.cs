using ExpenseTracker.Core.Domain.Entities;
using ExpenseTracker.Core.DTOs;
using ExpenseTracker.Core.Service_Interfaces;
using ExpenseTracker.Core.ValidationHelpers;

namespace ExpenseTracker.Core.Service_Classes
{
    public class UserService : IUserService
    {
        private  List<User> _users;

        public UserService() {
            _users = new List<User>();
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="userRequest"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<UserResponse> CreateUser(UserRequest userRequest)
        {
            if(userRequest == null)
            {
                throw new ArgumentNullException(nameof(userRequest),"Request is null");
            }

            bool isModelValid = ValidateModel.IsModelValid(userRequest);
            if (!isModelValid)
            {
                throw new ArgumentException(ValidateModel.Errors, nameof(userRequest));
            }

            // check if email already exists
            var existingUser = _users.FirstOrDefault(u => u.Email == userRequest.Email);
            if (existingUser != null)
            {
                throw new ArgumentException("Email already exists", nameof(userRequest));
            }

            User user = userRequest.ToUser();
            user.UserId = Guid.NewGuid();
            _users.Add(user);
            return user.ToUserResponse();
        }

        public async Task<IEnumerable<UserResponse>> GetAllUsers()
        {
            if (!_users.Any())
            {
                return new List<UserResponse>();
            }
            var userResponses = _users.Select(u => u.ToUserResponse()).ToList();
            return userResponses;
        }

        public async Task<UserResponse?> GetUserByEmail(string userEmail)
        {
            if(string.IsNullOrEmpty(userEmail))
            {
                throw new ArgumentNullException(nameof(userEmail), "Email is null or empty");
            }
            var user = _users.FirstOrDefault(u => u.Email == userEmail);
            if (user == null)
            {
                return null;
            }
            return user.ToUserResponse();
        }

        public async Task<UserResponse?> GetUserById(Guid userId)
        {
            if(userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId), "UserId is null or empty");
            }
            var user = _users.FirstOrDefault(u => u.UserId == userId);
            if (user == null)
            {
                return null;
            }
            return user.ToUserResponse();
        }
    }
}
