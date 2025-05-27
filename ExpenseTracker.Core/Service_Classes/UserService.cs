using ExpenseTracker.Core.Domain.Entities;
using ExpenseTracker.Core.DTOs;
using ExpenseTracker.Core.Service_Interfaces;
using ExpenseTracker.Core.ValidationHelpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Core.Service_Classes
{
    public class UserService : IUserService
    {

        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager) {
            _userManager = userManager;
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
            if (userRequest == null)
            {
                throw new ArgumentNullException(nameof(userRequest), "Request is null");
            }

            bool isModelValid = ValidateModel.IsModelValid(userRequest);
            if (!isModelValid)
            {
                throw new ArgumentException(ValidateModel.Errors, nameof(userRequest));
            }

            // check if email already exists
            var existingUser = await _userManager.FindByEmailAsync(userRequest.Email);
            if (existingUser != null)
            {
                throw new ArgumentException("Email already exists", nameof(userRequest));
            }

            ApplicationUser user = userRequest.ToUser();
            var result = await _userManager.CreateAsync(user, userRequest.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"User creation failed: {errors}");
            }
            
            return user.ToUserResponse();
        }

        /// <summary>
        /// Get all the users
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<UserResponse>> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            if (users == null || users.Count == 0)
            {
                return new List<UserResponse>();  
            }
            return users.Select(u => u.ToUserResponse()).ToList();
        }

        /// <summary>
        /// Get user by email
        /// </summary>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<UserResponse?> GetUserByEmail(string userEmail)
        {
            if(string.IsNullOrEmpty(userEmail))
            {
                throw new ArgumentNullException(nameof(userEmail), "Email is null or empty");
            }
            var user = await _userManager.FindByEmailAsync(userEmail);
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
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return null;
            }
            return user.ToUserResponse();
        }
    }
}
