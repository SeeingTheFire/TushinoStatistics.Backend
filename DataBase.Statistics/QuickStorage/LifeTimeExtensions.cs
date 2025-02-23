namespace DataBase.Statistics.QuickStorage;

internal static class LifeTimeExtensions {
    public static TimeSpan AsTimeSpan(this int? value) => new TimeSpan(0, 0, value ?? 0);
    public static TimeSpan AsTimeSpan(this int value) => new TimeSpan(0, 0, value);
}