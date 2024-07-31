using System.ComponentModel.DataAnnotations;
using static TeisterMask.DataConstraints;

namespace TeisterMask.Data.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ProjectNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        public DateTime OpenDate { get; set; }

        public DateTime? DueDate { get; set; }
        public virtual ICollection<Task> Tasks { get; set; } = new HashSet<Task>();
    }
}
