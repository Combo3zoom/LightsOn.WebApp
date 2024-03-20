namespace LightsOn.BlazorApp.Brokers.DateTimes;

public class LocalMachineDateTimeBroker : IDateTimeBroker
{
    public DateTimeOffset GetCurrentDateTime() =>
        DateTimeOffset.UtcNow;
}