using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P01_StudentSystem.Data.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal Price { get; set; }

        public ICollection<Homework> Homeworks { get; set; } = new List<Homework>();
        public ICollection<Resource> Resources { get; set; } = new List<Resource>();
        public ICollection<StudentCourse> StudentsCourses { get; set; } = new List<StudentCourse>();

    }
}
