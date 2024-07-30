using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Theatre
{
    public static class DataConstraints
    {
        // THEATRE 

        public const byte TheatreNameMinLength = 4;
        public const byte TheatreNameMaxLength = 30;

        public const sbyte TheatreNumberOfHallsMinValue = 1;
        public const sbyte TheatreNumberOfHallsMaxValue = 10;

        public const byte TheatreDirectorMinLength = 4;
        public const byte TheatreDirectorMaxLength = 30;

        // PLAY 

        public const byte PlayTitleMinLength = 4;
        public const byte PlayTitleMaxLength = 50;

        public const double PlayRatingMinLength = 0.00;
        public const double PlayRatingMaxLength = 10.00;

        public const int PlayDescriptionMaxLength = 700;

        public const byte PlayScreenwriterMinLength = 4;
        public const byte PlayScreenwriterMaxLength = 30;

        // CAST 

        public const byte CastFullNameMinLength = 4;
        public const byte CastFullNameMaxLength = 30;

        public const string CastPhoneNumberRegexValidation = @"\+44-\d{2}-\d{3}-\d{4}";

        // TICKET 

        public const double TicketPriceMinValue = 1.00;
        public const double TicketPriceMaxValue = 100.00;

        public const sbyte TicketRowNumberMinValue = 1;
        public const sbyte TicketRowNumberMaxValue = 10;
    }
}
