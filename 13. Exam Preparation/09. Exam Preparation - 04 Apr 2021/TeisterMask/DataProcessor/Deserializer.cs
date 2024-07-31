// ReSharper disable InconsistentNaming

using System.Globalization;
using TeisterMask.Data.Models;
using TeisterMask.Data.Models.Enums;
using TeisterMask.DataProcessor.ImportDto;
using Task = TeisterMask.Data.Models.Task;

namespace TeisterMask.DataProcessor
{
    using System.ComponentModel.DataAnnotations;
    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    using Data;
    using System.Diagnostics.Metrics;
    using System.Text;
    using TeisterMask.Utilities;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedProject
            = "Successfully imported project - {0} with {1} tasks.";

        private const string SuccessfullyImportedEmployee
            = "Successfully imported employee - {0} with {1} tasks.";

        public static string ImportProjects(TeisterMaskContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();
            XmlHelper xmlHelper = new XmlHelper();

            const string xmlRoot = "Projects";

            ICollection<Project> projectsToImport = new List<Project>();


            ImportProjectsDto[] deserializedProjects = xmlHelper.Deserialize<ImportProjectsDto[]>(xmlString, xmlRoot);

            foreach (ImportProjectsDto projectDto in deserializedProjects)
            {
                if (!IsValid(projectDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                bool isProjectOpenDateValid = DateTime.TryParseExact(projectDto.OpenDate, "dd/MM/yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime openDate);

                bool isProjectDueDateValid = DateTime.TryParseExact(projectDto.DueDate, "dd/MM/yyyy",
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dueDate);
                

                if (!isProjectOpenDateValid)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Project newProject = new Project()
                {
                    Name = projectDto.Name,
                    OpenDate = openDate,
                    DueDate = dueDate
                };

                ICollection<Task> tasksToImport = new List<Task>();


                foreach (ImportTasksDto taskDto in projectDto.Tasks)
                {

                    if (!IsValid(taskDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    bool isTaskOpenDateValid = DateTime.TryParseExact(taskDto.OpenDate, "dd/MM/yyyy",
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime taskOpenDate);

                    bool isTaskDueDateValid = DateTime.TryParseExact(taskDto.DueDate, "dd/MM/yyyy",
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime taskDueDate);

                    if (!isTaskOpenDateValid || !isTaskDueDateValid || taskOpenDate < openDate)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    if (isProjectDueDateValid && taskDueDate > dueDate)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    ExecutionType executionType;

                    if (taskDto.ExecutionType == 0)
                    {
                        executionType = ExecutionType.ProductBacklog;
                    }
                    else if (taskDto.ExecutionType == 1)
                    {
                        executionType = ExecutionType.SprintBacklog;
                    }
                    else if (taskDto.ExecutionType == 2)
                    {
                        executionType = ExecutionType.InProgress;
                    }
                    else if (taskDto.ExecutionType == 3)
                    {
                        executionType = ExecutionType.Finished;
                    }
                    else
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    LabelType labelType;

                    if (taskDto.LabelType == 0)
                    {
                        labelType = LabelType.Priority;
                    }
                    else if (taskDto.LabelType == 1)
                    {
                        labelType = LabelType.CSharpAdvanced;
                    }
                    else if (taskDto.LabelType == 2)
                    {
                        labelType = LabelType.JavaAdvanced;
                    }
                    else if (taskDto.LabelType == 3)
                    {
                        labelType = LabelType.EntityFramework;
                    }
                    else if (taskDto.LabelType == 4)
                    {
                        labelType = LabelType.Hibernate;
                    }
                    else
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    Task newTask = new Task()
                    {
                        Name = taskDto.Name,
                        OpenDate = taskOpenDate,
                        DueDate = taskDueDate,
                        ExecutionType = executionType,
                        LabelType = labelType
                    };

                    tasksToImport.Add(newTask);
                }

                newProject.Tasks = tasksToImport;
                projectsToImport.Add(newProject);

                sb.AppendLine(string.Format(SuccessfullyImportedProject, newProject.Name, newProject.Tasks.Count));

            }

            context.Projects.AddRange(projectsToImport);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportEmployees(TeisterMaskContext context, string jsonString)
        {
            throw new NotImplementedException();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}