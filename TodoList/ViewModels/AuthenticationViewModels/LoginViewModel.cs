using System.ComponentModel.DataAnnotations;

namespace TodoList.Web.ViewModels.AuthenticationViewModels
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
    }
}
