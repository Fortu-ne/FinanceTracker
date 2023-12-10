using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.Dto
{
    public class LoginDto
    {
        [EmailAddress]
        public string Email { get; set; } = String.Empty;

        public string Username { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;

    }

}
