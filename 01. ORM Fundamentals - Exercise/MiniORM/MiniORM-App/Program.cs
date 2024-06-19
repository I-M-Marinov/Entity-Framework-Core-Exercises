using MiniORM.App;
using MiniORM.App.Entities;

var connectionString = "Server=MARINOV-GAME-PC\\SQLEXPRESS;Database=MiniORM;Integrated Security=true;Encrypt=False;TrustServerCertificate=true;";
var context = new SoftUniDbContext(connectionString);

// Add
context.Employees.Add(new Employee
{
    FirstName = "Ivan",
    LastName = "Marinov",
    DepartmentId = context.Departments.First().Id,
    IsEmployed = false

});

context.Departments.Add(new Department { Name = "Human Resources" });
context.Departments.Add(new Department { Name = "HR" });

context.SaveChanges();

// Remove


var departmentToDelete = context.Departments.FirstOrDefault(d => d.Name == "HR");

if (departmentToDelete is not null)
{
    context.Departments.Remove(departmentToDelete);
}

context.SaveChanges();

// Update

var employeesToUpdate = context.Employees.Where(e => e.LastName == "Porter").ToList();
foreach (var employee in employeesToUpdate)
{
    employee.LastName = "Parker";
}

context.SaveChanges();