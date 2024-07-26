using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cadastre.Data.Enumerations;

namespace Cadastre
{
    public static class DataConstraints
    {
        // DISTRICT
        public const byte DistrictNameMinLength = 2;
        public const byte DistrictNameMaxLength = 80;

        public const byte DistrictPostalCodeMinLength = 8;
        public const byte DistrictPostalCodeMaxLength = 8;

        public static string[] DistrictRegionValidValues = { "SouthEast" , "SouthWest", "NorthEast", "NorthWest" };




        // CITIZEN 

        public const byte CitizenFirstNameMinLength = 2;
        public const byte CitizenFirstNameMaxLength = 30;

        public const byte CitizenLastNameMinLength = 2;
        public const byte CitizenLastNameMaxLength = 30;

        // PROPERTY 

        public const byte PropertyIdentifierMinLength = 16;
        public const byte PropertyIdentifierMaxLength = 20;

        public const int PropertyDetailsMinLength = 5;
        public const int PropertyDetailsMaxLength = 500;

        public const byte PropertyAddressMinLength = 5;
        public const byte PropertyAddressMaxLength = 200;

        public const string DistrictPostalCodeRegexValidation = @"^[A-Z]{2}-\d{5}$";
    }
}
