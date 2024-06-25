using Microsoft.EntityFrameworkCore;
using P01_StudentSystem.Data;

public class StartUp
{
    public static void Main(string[] args)
    {

        var options = new DbContextOptionsBuilder<StudentSystemContext>()
            .UseSqlServer(@"Server=MARINOV-GAME-PC\\SQLEXPRESS; Database = BlogDb; Integrated Security = true; Encrypt = False; TrustServerCertificate = true;")
            .Options;

        var context = new StudentSystemContext();


    }
}