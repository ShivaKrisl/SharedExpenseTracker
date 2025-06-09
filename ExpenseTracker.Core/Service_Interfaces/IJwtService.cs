using ExpenseTracker.Core.Domain.Entities;
using ExpenseTracker.Core.DTOs;

namespace ExpenseTracker.Core.Service_Interfaces
{
    public interface IJwtService
    {
        public Task<AuthenticationResponse> CreateJwtToken(ApplicationUser user);
    }
}
