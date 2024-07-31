using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeisterMask.Data.Models
{
    public class EmployeeTask
    {
        [Required]
        [ForeignKey(nameof(Employee))]

        public int EmployeeId { get; set; }

        [Required] 
        public virtual Employee Employee { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Task))]
        public int TaskId { get; set; }

        [Required]
        public virtual Task Task { get; set; } = null!;

    }
}
