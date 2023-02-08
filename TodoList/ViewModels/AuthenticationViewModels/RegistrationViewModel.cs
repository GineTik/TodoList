using System.ComponentModel.DataAnnotations;

namespace TodoList.Web.ViewModels.AuthenticationViewModels
{
    public class RegistrationViewModel
    {
        [Required]
        [MaxLength(60)]
        public string Login { get; set; }

        [Required]
        [MaxLength(60)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
