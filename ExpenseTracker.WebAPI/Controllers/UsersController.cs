using ExpenseTracker.Core.Domain.Entities;
using ExpenseTracker.Core.DTOs;
using ExpenseTracker.Core.Service_Interfaces;
using ExpenseTracker.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.WebAPI.Controllers
{
  
    public class UsersController : CustomBaseController
    {
        private readonly IUserService _userService;

        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(IUserService userService, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _userService = userService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [Authorize]
        //[ValidateAntiForgeryToken]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("id/{userId:guid}")]
        [Authorize]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> GetUserById(Guid userId)
        {
            try
            {
                var user = await _userService.GetUserById(userId);
                if (user == null)
                {
                    return NotFound("User Not Found");
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("email/{email}")]
        [Authorize]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            try
            {
                var user = await _userService.GetUserByEmail(email);
                if (user == null)
                {
                    return NotFound($"User with email {email} not found!!");
                }
                return Ok(user);
            }catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser([FromBody] UserRequest userRequest)
        {
            if (userRequest == null)
            {
                return BadRequest("Request cannot be Empty");
            }
            try
            {
                var user = await _userService.CreateUser(userRequest);
                return CreatedAtAction(nameof(GetUserById), new { userId = user.UserId },user);
            }catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            return Unauthorized("Please Login or Register first");
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginUser(LoginDTO loginDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    string errors = string.Join("\n", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList());
                    return BadRequest(errors);
                }

                var user = await _userManager.FindByEmailAsync(loginDTO.Email);
                if(user == null)
                {
                    return NotFound($"User not found with Email id: {loginDTO.Email}");
                }

                var result = await _signInManager.PasswordSignInAsync(user, loginDTO.Password, isPersistent: false, lockoutOnFailure: false);
                if (!result.Succeeded)
                {
                    return Unauthorized("Invalid Email or Password");
                }
                return Ok("Login Successfull!!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
