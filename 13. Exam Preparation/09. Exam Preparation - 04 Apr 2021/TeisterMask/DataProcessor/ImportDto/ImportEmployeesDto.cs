
using System.ComponentModel.DataAnnotations;
using static TeisterMask.DataConstraints;

namespace TeisterMask.DataProcessor.ImportDto
{
    public class ImportEmployeesDto
    {
        [Required]
        [RegularExpression(EmployeeUsernameRegexValidation)]
        public string Username { get; set; } = null!;

        [EmailAddress]
        [Required]
        public string Email { get; set; } = null!;

        [Required]
        [RegularExpression(EmployeePhoneRegexValidation)]
        public string Phone { get; set; } = null!;


        public int[] Tasks { get; set; } 
    }
}
