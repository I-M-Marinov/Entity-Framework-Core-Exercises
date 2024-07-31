
using System.Xml.Serialization;
using TeisterMask.Data.Models;
using Task = TeisterMask.Data.Models.Task;

namespace TeisterMask.DataProcessor.ExportDto
{
    [XmlType(nameof(Project))]

    public class ExportProjectsDto
    {

        [XmlAttribute(nameof(TasksCount))]
        public int TasksCount { get; set; }

        [XmlElement(nameof(ProjectName))]
        public string ProjectName { get; set; } = null!;


        [XmlElement(nameof(HasEndDate))]
        public string HasEndDate { get; set; } = null!;

        [XmlArray(nameof(Tasks))]
        [XmlArrayItem(nameof(Task))]

        public ExportTasksXmlDto[] Tasks { get; set; } 
    }
}
