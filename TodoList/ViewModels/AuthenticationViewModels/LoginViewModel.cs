using System.ComponentModel.DataAnnotations;

namespace TodoList.ViewModels.AuthenticationViewModels
{
    public class LoginViewModel
    {
        [Required]
        [MaxLength(60)]
        public string Login { get; set; }

        [Required]
        [MaxLength(60)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
