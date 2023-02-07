using System.ComponentModel.DataAnnotations;

namespace TodoList.Core.Entities
{
    public class Role
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(60)]
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
