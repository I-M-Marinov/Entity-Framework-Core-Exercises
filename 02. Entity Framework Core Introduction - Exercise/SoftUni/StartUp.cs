using System.Text;
using Microsoft.EntityFrameworkCore;
using SoftUni.Data;
using SoftUni.Models;


namespace SoftUni
{
    public class StartUp

    {
        public static void Main(string[] args)
        {

            var context = new SoftUniContext();

            /* EXERCISE 3  */

            //using (var context = new SoftUniContext())
            //{
            //    string employeeInfo = SoftUniContext.GetEmployeesFullInformation(context);

            //    Console.WriteLine(employeeInfo);
            //}

            /* EXERCISE 4  */


            //using (var context = new SoftUniContext())
            //{
            //    string info = SoftUniContext.GetEmployeesWithSalaryOver50000(context);

            //    Console.WriteLine(info);
            //}


            /* EXERCISE 5  */

            //using (var context = new SoftUniContext())
            //{
            //    string info = SoftUniContext.GetEmployeesFromResearchAndDevelopment(context);

            //    Console.WriteLine(info);
            //}


            /* EXERCISE 6
             
             6.	Adding a New Address and Updating Employee
               NOTE: You will need method public static string AddNewAddressToEmployee(SoftUniContext context) and public StartUp class. 
               Create a new address with the text "Vitoshka 15" and TownId = 4. Set that address to the employee with last the name "Nakov".
               Then order by descending all the employees by their Address' Id, take 10 rows and from them, take the AddressText. Return the results each on a new line:
               Example
               Output

               Vitoshka 15
               163 Nishava Str, ent A, apt. 1
               …

               After this restore your database for the tasks ahead!
            
            */

            //using (var context = new SoftUniContext())
            //{
            //    Console.WriteLine(SoftUniContext.AddNewAddressToEmployee(context));
            //}

            /* EXERCISE 7
             7.	Employees and Projects

                NOTE: You will need method public static string GetEmployeesInPeriod(SoftUniContext context) and public StartUp class. 
                Find the first 10 employees and print each employee's first name, last name, manager's first name and last name. 
                If they have projects started in the period 2001 - 2003 (inclusive), print them with information about their name, start and end date.
                Then return all of their projects in the format "--<ProjectName> - <StartDate> - <EndDate>", each on a new row.
                If a project has no end date, print "not finished" instead.

               Constraints

               Use date format: "M/d/yyyy h:mm:ss tt".
               Example
               Output

               Guy Gilbert - Manager: Jo Brown

               --Half-Finger Gloves - 6/1/2002 12:00:00 AM - 6/1/2003 12:00:00 AM
               --Women's Tights - 6/1/2002 12:00:00 AM - 6/1/2003 12:00:00 AM

               Kevin Brown - Manager: David Bradley
               Roberto Tamburello – Manager: Teeri Duffy

               --Classic Vest – 6.1.2003 12:00:00 - not finished
             
             */

            //using (var context = new SoftUniContext())
            //{
            //    Console.WriteLine(SoftUniContext.GetEmployeesInPeriod(context));
            //}


            /* EXERCISE 8
                8.	Addresses by Town
               NOTE: You will need method public static string GetAddressesByTown(SoftUniContext context) and public StartUp class. 
               Find all addresses, ordered by the number of employees who live there (descending), 
            then by town name (ascending) and finally by address text (ascending). 
            Take only the first 10 addresses. For each address return it in the format "<AddressText>, <TownName> - <EmployeeCount> employees"
               Example

               Output

               7726 Driftwood Drive, Monroe - 2 employees
               163 Nishava Str, ent A, apt. 1, Sofia - 2 employees

             */

            //using (context)
            //{
            //    Console.WriteLine(GetAddressesByTown(context));
            //}


            /* EXERCISE 9

             9.	Employee 147
               NOTE: You will need method public static string GetEmployee147(SoftUniContext context) and public StartUp class. 

               Get the employee with id 147. Return only his/her first name, last name, job title and projects (print only their names). 
                The projects should be ordered by name (ascending). Format of the output.
               Example
               Output

               Linda Randall - Production Technician
               HL Touring Handlebars
               …
               
             */

            //using (context)
            //{
            //    Console.WriteLine(GetEmployee147(context));
            //}

            /* EXERCISE 10

             10.	Departments with More Than 5 Employees
               NOTE: You will need method public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context) and public StartUp class. 
               Find all departments with more than 5 employees. Order them by employee count (ascending), then by department name (alphabetically). 
            For each department, print the department name and the manager's first and last name on the first row. 
            Then print the first name, the last name and the job title of every employee on a new row. 
            Order the employees by first name (ascending), then by last name (ascending).
               Format of the output: 
            For each department print it in the format "<DepartmentName> - <ManagerFirstName>  <ManagerLastName>" and for each employee print it in the format 
            "<EmployeeFirstName> <EmployeeLastName> - <JobTitle>".

               Example
               Output

               Engineering – Terri Duffy
               Gail Erickson - Design Engineer
               Jossef Goldberg - Design Engineer
               …
               
             */


            //using (context)
            //{
            //    Console.WriteLine(GetDepartmentsWithMoreThan5Employees(context));
            //}


            /* EXERCISE 11

            11.	Find Latest 10 Projects
               NOTE: You will need method public static string GetLatestProjects(SoftUniContext context) and public StartUp class. 

               Write a program that returns information about the last 10 started projects.
                Sort them by name lexicographically and return their name, description and start date, each on a new row. 
               Constraints
               Use date format: "M/d/yyyy h:mm:ss tt".

               Example
               Output

                All-Purpose Bike Stand
                Research, design and development of All-Purpose Bike Stand. 
                Perfect all-purpose bike stand for working on your bike at home. 
                Quick-adjusting clamps and steel construction.
               9/1/2005 12:00:00 AM
               …
               

             */


            //using (context)
            //{
            //    Console.WriteLine(GetLatestProjects(context));
            //}

            /* EXERCISE 12

            12.	Increase Salaries
                NOTE: You will need method public static string IncreaseSalaries(SoftUniContext context) and public StartUp class. 

                Write a program that increases salaries of all employees that are in the Engineering, Tool Design, Marketing, 
                or Information Services department by 12%. Then return first name, last name and salary (2 symbols after the decimal separator) for those employees, 
                whose salary was increased. Order them by first name (ascending), then by last name (ascending). 
                Format of the output.

               Example

               Output
               Ashvini Sharma ($36400.00)
               Dan Bacon ($30688.00)
               ..................................
               

             */

            //using (context)
            //{
            //    Console.WriteLine(IncreaseSalaries(context));
            //}


            /*
             13.	Find Employees by First Name Starting with "Sa"
               NOTE: You will need method public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context) and public StartUp class. 

            Write a program that finds all employees whose first name starts with "Sa". 
            Return their first, last name, their job title and salary rounded to 2 symbols after the decimal separator in the format given in the example below.
            Order them by the first name, then by last name (ascending).
               Constraints

               Find a way to make your query case-insensitive.	
               Example
               Output

               Sairaj Uddin - Scheduling Assistant - ($16000.00)
               Samantha Smith - Production Technician - ($14000.00)
               ......................................
               
             */

            using (context)
            {
                Console.WriteLine(GetEmployeesByFirstNameStartingWithSa(context));
            }

        }
                                                /* EXERCISE 3  */
        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            var employees = context.Employees
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.MiddleName,
                    e.JobTitle,
                    e.Salary,
                    e.EmployeeId
                })
                .OrderBy(e => e.EmployeeId)
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.MiddleName,
                    e.JobTitle,
                    Salary = e.Salary.ToString("F2")
                });

            var result = employees.Select(e => $"{e.FirstName} {e.LastName} {e.MiddleName} {e.JobTitle} {e.Salary}");

            return string.Join(Environment.NewLine, result);
        }

                                                /* EXERCISE 4  */
        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            var employees = context.Employees
                .Where(e => e.Salary > 50000)
                .Select(e => new
                {
                    e.FirstName,
                    Salary = e.Salary.ToString("F2")
                })
                .OrderBy(e => e.FirstName)
                .ToList();

            var result = employees.Select(e => $"{e.FirstName} - {e.Salary}");

            return string.Join(Environment.NewLine, result);
        }

                                                /* EXERCISE 5  */
        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            var employees = context.Employees
                .Where(e => e.Department.Name == "Research and Development")
                .OrderBy(e => e.Salary)
                .ThenByDescending(e => e.FirstName)
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    DepartmentName = e.Department.Name,
                    Salary = e.Salary.ToString("F2")
                })
                .ToList();

            var result = employees.Select(e => $"{e.FirstName} {e.LastName} from {e.DepartmentName} - ${e.Salary}");

            return string.Join(Environment.NewLine, result);
        }

                                                /* EXERCISE 6  */
        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            var newAddress = new Address();
            newAddress.AddressText = "Vitoshka 15";
            newAddress.TownId = 4;

            context.Addresses.Add(newAddress);
            context.SaveChanges();

            var employeeId = context.Employees
                .Where(e => e.LastName == "Nakov")
                .Select(e => e.EmployeeId)
                .FirstOrDefault();

            var employee = context.Employees.Find(employeeId);

            employee.AddressId = newAddress.AddressId;

            context.SaveChanges();


            var addressTexts = context.Employees
                .OrderByDescending(e => e.AddressId)
                .Take(10)
                .Select(e => e.Address.AddressText)
                .ToList();

            return string.Join(Environment.NewLine, addressTexts);

        }
            
                                                /* EXERCISE 7  */
        public static string GetEmployeesInPeriod(SoftUniContext context)
        {
            var result = context.Employees
                .Take(10)
                .Select(e => new
                {
                    EmployeeNames = $"{e.FirstName} {e.LastName}",
                    ManagerNames = $"{e.Manager.FirstName} {e.Manager.LastName}",
                    Projects = e.EmployeesProjects
                        .Where(ep => ep.Project.StartDate.Year >= 2001 &&
                                     ep.Project.StartDate.Year <= 2003)
                        .Select(ep => new
                        {
                            ProjectName = ep.Project.Name,
                            ep.Project.StartDate,
                            EndDate = ep.Project.EndDate.HasValue ? ep.Project.EndDate.Value.ToString("M/d/yyyy h:mm:ss tt") : "not finished"
                        })
                });

            var sb = new StringBuilder();

            foreach (var e in result)
            {
                sb.AppendLine($"{e.EmployeeNames} - Manager: {e.ManagerNames}");
                if (e.Projects.Any())
                {
                    foreach (var p in e.Projects)
                    {
                        sb.AppendLine($"--{p.ProjectName} - {p.StartDate:M/d/yyyy h:mm:ss tt} - {p.EndDate}");
                    }
                }
            }

            return sb.ToString().TrimEnd();
        }

                                                /* EXERCISE 8  */
        public static string GetAddressesByTown(SoftUniContext context)
        {
            var result = context.Addresses
                .Select(addr => new
                {
                    addr.AddressText,
                    TownName = addr.Town.Name,
                    EmployeeCount = addr.Employees.Count
                })
                .OrderByDescending(addr => addr.EmployeeCount)
                .ThenBy(addr => addr.TownName)
                .ThenBy(addr => addr.AddressText)
                .Take(10)
                .ToList();

            var sb = new StringBuilder();

            foreach (var e in result)
            {
                sb.AppendLine($"{e.AddressText}, {e.TownName} - {e.EmployeeCount} employees");

            }

            return sb.ToString().TrimEnd();
        }

                                                /* EXERCISE 9  */
        public static string GetEmployee147(SoftUniContext context)
        {
            var employee = context.Employees
                .Where(e => e.EmployeeId == 147)
                .Select(e => new
                {
                    FullName = $"{e.FirstName} {e.LastName}",
                    e.JobTitle,
                    Projects = e.EmployeesProjects
                        .OrderBy(p => p.Project.Name)
                        .Select(p => new
                        {
                            p.Project.Name
                        })
                        .ToList()
                })
                .FirstOrDefault();

            var sb = new StringBuilder();

            sb.AppendLine($"{employee.FullName} - {employee.JobTitle}");

            if (employee.Projects.Any())
            {
                foreach (var project in employee.Projects)
                {
                    sb.AppendLine($"{project.Name}");
                }
            }

            return sb.ToString().TrimEnd();
        }

                                                /* EXERCISE 10  */
        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        {
            var departments = context.Departments
                .Where(d => d.Employees.Count > 5)
                .Select(d => new
                {
                    DepartmentName = d.Name,
                    ManagerFullName = $"{d.Manager.FirstName} {d.Manager.LastName}",
                    EmployeeCount = d.Employees.Count,
                })
                .OrderBy(e => e.EmployeeCount)
                .ThenBy(d => d.DepartmentName)
                .ToList();

            var employees = context.Employees
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .Select(e => new
                {
                    FullName = $"{e.FirstName} {e.LastName}",
                    e.JobTitle,
                    e.Department
                    })
                .ToList();

           var sb = new StringBuilder();

            foreach (var department  in departments)
            {
                sb.AppendLine($"{department.DepartmentName} - {department.ManagerFullName}");

                foreach (var employee in employees)
                {
                    if (employee.Department.Name == department.DepartmentName)
                    {
                        sb.AppendLine($"{employee.FullName} - {employee.JobTitle}");
                    }
                }
            }

            return sb.ToString().TrimEnd();
        }

                                                /* EXERCISE 11  */
        public static string GetLatestProjects(SoftUniContext context)
        {
            var projects = context.Projects
                .OrderByDescending(p => p.StartDate)
                .Take(10)
                .Select(p => new
                {
                    p.Name,
                    p.Description,
                    p.StartDate,
                })
                .OrderBy(p => p.Name)
                .ToList();

            var sb = new StringBuilder();

            foreach (var project in projects)
            {
                sb.AppendLine($"{project.Name}");
                sb.AppendLine($"{project.Description}");
                sb.AppendLine($"{project.StartDate:M/d/yyyy h:mm:ss tt}");
            }


            return sb.ToString().TrimEnd();
        }

                                                /* EXERCISE 12  */
        public static string IncreaseSalaries(SoftUniContext context)
        {
            string[] validDepartments = {"Engineering", "Tool Design", "Marketing", "Information Services" };

            var employees = context.Employees
                .Where(e => validDepartments.Contains(e.Department.Name)).ToList();

            var employeesToPrint = employees
            .Select(e => new
            {
                e.FirstName,
                e.LastName,
                e.Salary 
            }).OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName);

            var sb = new StringBuilder();

            foreach (var employee in employeesToPrint)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} (${(employee.Salary * 1.12m):F2})");
            }

            context.SaveChanges();

            return sb.ToString().TrimEnd();

        }

                                                /* EXERCISE 13  */
        public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
        {

            var employees = context.Employees
                .Where(e => e.FirstName.StartsWith("Sa"))
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.JobTitle,
                    e.Salary
                }).OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName);

            var sb = new StringBuilder();

            foreach (var e in employees)
            {
                sb.AppendLine($"{e.FirstName} {e.LastName} - {e.JobTitle} - (${e.Salary:F2})");
            }

            return sb.ToString().TrimEnd();

        }
    }
}
