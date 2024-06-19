using System.Security.Cryptography;

namespace MiniORM
{
    public class DbContext
    {
        internal static List<Type> AllowedSqlTypes { get; } = new List<Type>() {
            typeof(string), 
            typeof(int), 
            typeof(uint), 
            typeof(long), 
            typeof(decimal), 
            typeof(bool), 
            typeof(DateTime)
        };


}
}
