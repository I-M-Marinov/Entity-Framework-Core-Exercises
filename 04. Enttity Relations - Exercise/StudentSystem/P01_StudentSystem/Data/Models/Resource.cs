using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using P01_StudentSystem.Data.Enumerations;
using ResourceType = P01_StudentSystem.Data.Enumerations.ResourceType;


namespace P01_StudentSystem.Data.Models
{
    public class Resource
    {

        public int ResourceId { get; set; }

        public string Name { get; set; } = null!;

        public string Url { get; set; } = null!;

        public ResourceType ResourceType { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;

    }
}
