using System.ComponentModel.DataAnnotations;
using static TeisterMask.DataConstraints;
using System.Xml.Serialization;
using TeisterMask.Data.Models;
using Task = TeisterMask.Data.Models.Task;

namespace TeisterMask.DataProcessor.ImportDto
{
    [XmlType(nameof(Task))]
    public class ImportTasksDto
    {
        [Required] 
        [XmlElement(nameof(Name))]
        [MinLength(TaskNameMinLength)]
        [MaxLength(TaskNameMaxLength)]
        public string Name { get; set; } = null!;


        [Required]
        [XmlElement(nameof(OpenDate))]
        public string OpenDate { get; set; } = null!;

        [Required]
        [XmlElement(nameof(DueDate))]

        public string DueDate { get; set; } = null!;

        [Required]
        [Range(TaskExecutionTypeMinValue,TaskExecutionTypeMaxValue)]
        [XmlElement(nameof(ExecutionType))]
        public int ExecutionType { get; set; }

        [Required]
        [Range(TaskLabelTypeMinValue, TaskLabelTypeMaxValue)]
        [XmlElement(nameof(LabelType))]
        public int LabelType { get; set; }
    }
}
