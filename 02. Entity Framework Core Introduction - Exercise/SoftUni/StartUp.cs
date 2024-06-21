using Microsoft.EntityFrameworkCore;
using SoftUni.Data;
using SoftUni.Models;


namespace SoftUni
{
    public class StartUp

    {
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
        public static void Main(string[] args)
        {

            /* EXERCISE 3  */

            //using (var context = new SoftUniContext())
            //{
            //    string employeeInfo = GetEmployeesFullInformation(context);

            //    Console.WriteLine(employeeInfo);
            //}

            /* EXERCISE 4  */
            //using (var context = new SoftUniContext())
            //{
            //    string info = GetEmployeesWithSalaryOver50000(context);

            //    Console.WriteLine(info);
            //}


            /* EXERCISE 5  */

            //using (var context = new SoftUniContext())
            //{
            //    string info = GetEmployeesFromResearchAndDevelopment(context);

            //    Console.WriteLine(info);
            //}


            /* EXERCISE 6  */

            using (var context = new SoftUniContext())
            {
                Console.WriteLine(AddNewAddressToEmployee(context));
            }

        }
    }
}
