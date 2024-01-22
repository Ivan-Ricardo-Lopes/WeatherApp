namespace IvanLopes.Forecast.Application.Extensions
{
    public static class DateTimeOffsetExtensions
    {
        public static bool IsToday(this DateTimeOffset dateTimeOffset)
        {
            DateTimeOffset localDateTime = dateTimeOffset.ToLocalTime();
            return localDateTime.Date == DateTimeOffset.Now.Date;
        }
    }
}
