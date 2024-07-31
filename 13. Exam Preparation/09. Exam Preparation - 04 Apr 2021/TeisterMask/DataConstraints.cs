using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeisterMask.Data.Models.Enums;

namespace TeisterMask
{
    public static  class DataConstraints
    {

        // EMPLOYEE

        public const byte EmployeeUsernameMinLength = 3;
        public const byte EmployeeUsernameMaxLength = 40;

        public const string EmployeePhoneRegexValidation = @"^\d{3}-\d{3}-\d{4}$";

        // PROJECT 

        public const byte ProjectNameMinLength = 2;
        public const byte ProjectNameMaxLength = 40;

        // TASK 

        public const byte TaskNameMinLength = 2;
        public const byte TaskNameMaxLength = 40;

        public const int TaskExecutionTypeMinValue = (int)ExecutionType.ProductBacklog;
        public const int TaskExecutionTypeMaxValue = (int)ExecutionType.Finished;

        public const int TaskLabelTypeMinValue = (int)LabelType.Priority;
        public const int TaskLabelTypeMaxValue = (int)LabelType.Hibernate;
    }
}
