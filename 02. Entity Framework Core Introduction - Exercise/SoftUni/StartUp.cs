using Microsoft.EntityFrameworkCore;
using SoftUni.Data;
using SoftUni.Models;


namespace SoftUni
{
    public class StartUp

    {

        public static void Main(string[] args)
        {

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

            using (var context = new SoftUniContext())
            {
                Console.WriteLine(SoftUniContext.AddNewAddressToEmployee(context));
            }

            /*
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



        }
    }
}
