﻿using System.ComponentModel.DataAnnotations;

namespace TodoList.Core.Entities
{
    public class Task
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string Text { get; set; }

        public DateTime ExpirationTime { get; set; }

        public bool Completed { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}