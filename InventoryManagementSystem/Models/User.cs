using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; } = string.Empty;
        public string? FullName { get; set; }
        public string? Email { get; set; }
    }

    public class RegisterViewModel
    {
        [Required, StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required, DataType(DataType.Password), MinLength(6)]
        public string Password { get; set; } = string.Empty;

        [Required, DataType(DataType.Password), Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;

        public string? FullName { get; set; }
        [EmailAddress] public string? Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required] public string Username { get; set; } = string.Empty;
        [Required, DataType(DataType.Password)] public string Password { get; set; } = string.Empty;
        public string? ReturnUrl { get; set; }
    }
}
