using System.ComponentModel.DataAnnotations;
using static TeisterMask.DataConstraints;
using System.Xml.Serialization;
using TeisterMask.Data.Models;

namespace TeisterMask.DataProcessor.ImportDto
{
    [XmlType(nameof(Project))]
    public class ImportProjectsDto
    {
        [XmlElement(nameof(Name))]
        [Required]
        [MinLength(ProjectNameMinLength)]
        [MaxLength(ProjectNameMaxLength)]
        public string Name { get; set; } = null!;

        [XmlElement(nameof(OpenDate))]
        [Required]
        public string OpenDate { get; set; } = null!;

        [XmlElement(nameof(DueDate))]
        public string? DueDate { get; set; }

        [Required]
        [XmlArray(nameof(Tasks))]
        public ImportTasksDto[] Tasks { get; set; }

    }
}
