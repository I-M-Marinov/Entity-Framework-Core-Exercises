﻿
using System.ComponentModel.DataAnnotations;


namespace VaporStore.Data.Models
{
    public class Developer
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        public virtual ICollection<Game> Games { get; set; } = new HashSet<Game>();
    }
}
