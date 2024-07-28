using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trucks.Data.Models.Enums;

namespace Trucks
{
    public static class DataConstraints
    {
        // CLIENT 

        public const byte ClientNameMinLength = 3;
        public const byte ClientNameMaxLength = 40;

        public const byte ClientNationalityMinLength = 2;
        public const byte ClientNationalityMaxLength = 40;

        // DISPATCHER

        public const byte DispatcherNameMinLength = 2;
        public const byte DispatcherNameMaxLength = 40;


        // TRUCK 

        public const string TruckRegistrationNumberRegexValidation = @"^[A-Z]{2}\d{4}[A-Z]{2}$";

        public const int TruckTankCapacityMinValue = 950;
        public const int TruckTankCapacityMaxValue = 1420;

        public const int TruckCargoCapacityMinValue = 5000;
        public const int TruckCargoCapacityMaxValue = 29000;

        public const int TruckCategoryTypeMinValue = (int)CategoryType.Flatbed;
        public const int TruckCategoryTypeMaxValue = (int)CategoryType.Semi;

        public const int TruckMakeTypeMinValue = (int)MakeType.Daf;
        public const int TruckMakeTypeMaxValue = (int)MakeType.Volvo;

    }
}
