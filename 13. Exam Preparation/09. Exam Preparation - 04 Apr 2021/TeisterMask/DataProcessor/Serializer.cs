using System.Security.Cryptography.X509Certificates;
using TeisterMask.Data.Models.Enums;
using TeisterMask.DataProcessor.ExportDto;

namespace TeisterMask.DataProcessor
{
    using Data;
    using Newtonsoft.Json;
    using System.Globalization;
    using TeisterMask.Utilities;

    public class Serializer
    {

        public static string ExportProjectWithTheirTasks(TeisterMaskContext context)
        {


            XmlHelper xmlHelper = new XmlHelper();
            const string xmlRoot = "Projects";

            ExportProjectsDto[] projectsToExport = context.Projects
                .Where(p => p.Tasks.Any())
                .Select(p => new ExportProjectsDto()
                {
                    TasksCount = p.Tasks.Count(),
                    ProjectName = p.Name,
                    HasEndDate = p.DueDate.HasValue ? "Yes" : "No",
                    Tasks = p.Tasks
                        .Select(t => new ExportTasksXmlDto()
                        {
                            Name = t.Name,
                            Label = t.LabelType.ToString()
                        })
                        .OrderBy(t => t.Name)
                        .ToArray()
                })
                .AsEnumerable()
                .OrderByDescending(p => p.TasksCount)
                .ThenBy(p => p.ProjectName)
                .ToArray();

            return xmlHelper.Serialize(projectsToExport, xmlRoot);

        }

        public static string ExportMostBusiestEmployees(TeisterMaskContext context, DateTime date)
        {
            var employees = context.Employees
                .Where(e => e.EmployeesTasks.Any(et => et.Task.OpenDate >= date))
                .Select(e => new
                {
                    Username = e.Username,
                    Tasks = e.EmployeesTasks
                        .Where(et => et.Task.OpenDate >= date)
                        .OrderByDescending(et => et.Task.DueDate)
                        .ThenBy(et => et.Task.Name)
                        .Select(et => new
                        {
                            TaskName = et.Task.Name,
                            OpenDate = et.Task.OpenDate,
                            DueDate = et.Task.DueDate,
                            LabelType = et.Task.LabelType,
                            ExecutionType = et.Task.ExecutionType
                        })
                        .ToArray()
                })
                .AsEnumerable() 
                .OrderByDescending(e => e.Tasks.Length)
                .ThenBy(e => e.Username)
                .Select(e => new ExportEmployeesDto()
                {
                    Username = e.Username,
                    Tasks = e.Tasks
                        .Select(et => new ExportTaskDto()
                        {
                            TaskName = et.TaskName,
                            OpenDate = et.OpenDate.ToString("d", CultureInfo.InvariantCulture),
                            DueDate = et.DueDate.ToString("d", CultureInfo.InvariantCulture),
                            LabelType = et.LabelType.ToString(),
                            ExecutionType = et.ExecutionType.ToString()
                        })
                        .ToArray()
                })
                .Take(10)
                .ToArray();

            return JsonConvert.SerializeObject(employees, Formatting.Indented);
        }
    }
}