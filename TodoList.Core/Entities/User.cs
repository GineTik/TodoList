using System.ComponentModel.DataAnnotations;

namespace TodoList.Core.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(60)]
        public string Login { get; set; }

        [Required]
        [MaxLength(60)]
        public string Password { get; set; }

        [Required]
        public DateTime DateOfRegistrationUTC { get; set; }

        public bool Banned { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

        public ICollection<TodoTask> Tasks { get; set; }
    }
}
