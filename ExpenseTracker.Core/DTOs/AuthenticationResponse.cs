using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Core.DTOs
{
    public class AuthenticationResponse
    {
        public string? UserName { get; set; }

        public string? Email{ get; set; }

        public string? Token { get; set; }

        public DateTime? TokenExpirationTime { get; set; }
    }
}
