namespace FinanceTracker.Dto
{
    public class UserDto
    {
        public string Name { get; set; } = String.Empty;
        public string Surname { get; set; } = String.Empty;
        public string Username { get; set; } = String.Empty;

        public string Email { get; set; } = String.Empty;
        public DateOnly DOB { get; set; } 
        public string Password { get; set; } = String.Empty;

        public string ConfirmPassword { get;set; } = String.Empty;
        public Double MonthlySalary { get; set; }

    }

}
