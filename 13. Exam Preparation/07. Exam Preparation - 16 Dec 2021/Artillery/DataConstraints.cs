using Artillery.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artillery
{
    public static class DataConstraints
    {
        // COUNTRY

        public const byte CountryNameMinLength = 4;
        public const byte CountryNameMaxLength = 60;

        public const int CountryArmySizeMinValue = 50_000;
        public const int CountryArmySizeMaxValue = 10_000_000;

        // MANUFACTURER

        public const byte ManufacturerNameMinLength = 4;
        public const byte ManufacturerNameMaxLength = 40;

        public const byte ManufacturerFoundedMinLength = 10;
        public const byte ManufacturerFoundedMaxLength = 100;

        // SHELL 

        public const double ShellWeightMinValue = 2;
        public const double ShellWeightMaxValue = 1_680;

        public const byte ShellCaliberMinLength = 4;
        public const byte ShellCaliberMaxLength = 30;

        // GUN 

        public const int GunWeightMinValue = 100;
        public const int GunWeightMaxValue = 1_350_000;

        public const double GunBarrelLengthMinValue = 2.00;
        public const double GunBarrelLengthMaxValue = 35.00;

        public const int GunRangeMinValue = 1;
        public const int GunRangeMaxValue = 100_000;


    }
}
