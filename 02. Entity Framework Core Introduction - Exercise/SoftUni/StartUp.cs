using SoftUni.Data;


namespace SoftUni
{
    public class StartUp

    {
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


            using (var context = new SoftUniContext())
            {
                string employeeInfo = GetEmployeesFullInformation(context);

                Console.WriteLine(employeeInfo);
            }

           
        }
    }
}
