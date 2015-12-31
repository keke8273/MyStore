using System;

namespace MyStore.Common.Implementation
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime GetCurrentDateTimeUtc()
        {
            return DateTime.UtcNow;
        }
    }
}
