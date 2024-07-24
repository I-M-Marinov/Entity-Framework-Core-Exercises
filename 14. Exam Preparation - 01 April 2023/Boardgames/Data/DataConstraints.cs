using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boardgames.Data
{
    public static class DataConstraints
    {
        // SELLER 

        public const byte SellerNameMinLength = 5;
        public const byte SellerNameMaxLength = 20;

        public const byte SellerAddressMinLength = 2;
        public const byte SellerAddressMaxLength = 30;

        // CREATOR 

        public const byte CreatorFirstNameMinLength = 2;
        public const byte CreatorFirstNameMaxLength = 7;

        public const byte CreatorLastNameMinLength = 2;
        public const byte CreatorLastNameMaxLength = 7;

        // BOARDGAME

        public const byte BoardGameNameMinLength = 10;
        public const byte BoardGameNameMaxLength = 20;

        public const string BoardGameRatingMinValue = "1.00";
        public const string BoardGameRatingMaxValue = "10.00";

        public const int BoardGameYearPublishedMinValue = 2018;
        public const int BoardGameYearPublishedMaxValue = 2023;
    }
}
