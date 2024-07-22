using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices.Data.Models
{
    public static class DataConstraints
    {

        public const byte ProductNameMinLength = 9;
        public const byte ProductNameMaxLength = 30;
        public const string ProductPriceMinValue = "5.00";
        public const string ProductPriceMaxValue = "1000.00";

        public const byte AddressStreetNameMinLength = 10;
        public const byte AddressStreetNameMaxLength = 20;

        public const byte AddressCityMinLength = 5;
        public const byte AddressCityMaxLength = 15;
        // ADDRESS
        public const byte AddressCountryMinLength = 5;
        public const byte AddressCountryMaxLength = 15;
        // INVOICE 
        public const int InvoiceNumberMinValue = 1_000_000_000;
        public const int InvoiceNumberMaxValue = 1_500_000_000;
    }
}
