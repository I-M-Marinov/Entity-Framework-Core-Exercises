using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;
using TravelAgency.Data;
using TravelAgency.Data.Models;
using TravelAgency.DataProcessor.ImportDtos;
using TravelAgency.Utilities;

namespace TravelAgency.DataProcessor
{
    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data format!";
        private const string DuplicationDataMessage = "Error! Data duplicated.";
        private const string SuccessfullyImportedCustomer = "Successfully imported customer - {0}";
        private const string SuccessfullyImportedBooking = "Successfully imported booking. TourPackage: {0}, Date: {1}";

        public static string ImportCustomers(TravelAgencyContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();

            XmlHelper xmlHelper = new XmlHelper();
            const string xmlRoot = "Customers";


            ICollection<Customer> customersToImport = new List<Customer>();

            ImportCustomersDto[] deserializedCustomers =
                xmlHelper.Deserialize<ImportCustomersDto[]>(xmlString, xmlRoot);

            foreach (ImportCustomersDto customerDto in deserializedCustomers)
            {
                if (!IsValid(customerDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (customersToImport.Any(c => c.FullName == customerDto.FullName) ||
                    customersToImport.Any(c => c.Email == customerDto.Email) ||
                    customersToImport.Any(c => c.PhoneNumber == customerDto.PhoneNumber))
                {
                    sb.AppendLine(DuplicationDataMessage);
                    continue;
                }

                Customer newCustomer = new Customer()
                {
                    FullName = customerDto.FullName,
                    Email = customerDto.Email,
                    PhoneNumber = customerDto.PhoneNumber
                };

                customersToImport.Add(newCustomer);
                sb.AppendLine(string.Format(SuccessfullyImportedCustomer, newCustomer.FullName));
            }

            context.Customers.AddRange(customersToImport);
            context.SaveChanges();

            return sb.ToString();
        }

        public static string ImportBookings(TravelAgencyContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();

            ICollection<Booking> bookingsToImport = new List<Booking>();

            ImportBookingsDto[] deserializedBookings = JsonConvert.DeserializeObject<ImportBookingsDto[]>(jsonString)!;

            foreach (ImportBookingsDto bookingDto in deserializedBookings)
            {
                if (!IsValid(bookingDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                bool isBookingDateValid = DateTime.TryParseExact(bookingDto.BookingDate, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out DateTime bookingDate);

                if (!isBookingDateValid)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                TourPackage tourPackage = context.TourPackages.FirstOrDefault(tp => tp.PackageName == bookingDto.TourPackageName)!;
                Customer customer = context.Customers.FirstOrDefault(c => c.FullName == bookingDto.CustomerName)!;

                Booking newBooking = new Booking()
                {
                    BookingDate = bookingDate,
                    Customer = customer,
                    TourPackage = tourPackage
                };


                bookingsToImport.Add(newBooking);
                sb.AppendLine(string.Format(SuccessfullyImportedBooking, newBooking.TourPackage.PackageName, newBooking.BookingDate.ToString("yyyy-MM-dd")));

            }

            context.Bookings.AddRange(bookingsToImport);
            context.SaveChanges();

            return sb.ToString();
        }

        public static bool IsValid(object dto)
        {
            var validateContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(dto, validateContext, validationResults, true);

            foreach (var validationResult in validationResults)
            {
                string currValidationMessage = validationResult.ErrorMessage;
            }

            return isValid;
        }
    }
}
