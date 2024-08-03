using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency
{
    public static class DataConstraints
    {
        // CUSTOMER 

        public const byte CustomerFullNameMinLength = 4;
        public const byte CustomerFullNameMaxLength = 60;


        public const byte CustomerEmailMinLength = 6;
        public const byte CustomerEmailMaxLength = 50;

        // GUIDE 

        public const byte GuideFullNameMinLength = 4;
        public const byte GuideFullNameMaxLength = 60;

        // TOUR PACKAGE 

        public const byte TourPackageNameMinLength = 2;
        public const byte TourPackageNameMaxLength = 40;

        public const byte TourPackageDescriptionMaxLength = 200;

    }
}
