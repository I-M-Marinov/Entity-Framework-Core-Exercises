using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Footballers
{
    public static class DataConstraints
    {
        // TEAM 

        public const byte TeamNameMinLength = 3;
        public const byte TeamNameMaxLength = 40;

        public const string TeamNameRegexValidation = "^[a-zA-Z0-9 .-]+$";

        public const byte TeamNationalityMinLength = 2;
        public const byte TeamNationalityMaxLength = 40;

        // COACH 

        public const byte CoachNameMinLength = 2;
        public const byte CoachNameMaxLength = 40;

        // FOOTBALLER

        public const byte FootballerNameMinLength = 2;
        public const byte FootballerNameMaxLength = 40;
    }
}
