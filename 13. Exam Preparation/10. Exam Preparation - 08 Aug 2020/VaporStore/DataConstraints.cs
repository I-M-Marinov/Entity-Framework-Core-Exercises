using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaporStore.Data.Models.Enums;

namespace VaporStore
{
    public static class DataConstraints
    {

        public const int CardTypeMinValue = (int)CardType.Debit;
        public const int CardTypeMaxValue = (int)CardType.Credit;


        public const int PurchaseTypeMinValue = (int)PurchaseType.Retail;
        public const int PurchaseTypeMaxValue = (int)PurchaseType.Digital;
    }
}
