namespace EventmiWorkshopMVC.Common
{
    public static class EntityConstraints
    {
        public static class Event
        {
            public const byte EventNameMinLength = 5;
            public const byte EventNameMaxLength = 50;

            public const byte EventPlaceMinLength = 3;
            public const byte EventPlaceMaxLength = 75;
        }
    }
}
