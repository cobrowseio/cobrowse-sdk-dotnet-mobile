using System;
using Android.Runtime;

namespace Cobrowse.IO;

[Preserve(AllMembers = true)]
internal static class DateTimeExtensions
{
    /// <summary>
    /// Convert <see cref="Java.Util.Date"/> to <see cref="DateTime"/>.
    /// </summary>
    internal static DateTime ToDateTime(this Java.Util.Date? javaDate)
    {
        // convert Java.Util.Date to DateTime
        if (javaDate == null)
        {
            return DateTime.MinValue;
        }
        long milliseconds = javaDate.Time;
        DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        DateTime dateTime = epoch.AddMilliseconds(milliseconds);
        return dateTime.ToLocalTime();
    }

    /// <summary>
    /// Convert <see cref="Java.Util.Date"/> to nullable <see cref="DateTime"/>.
    /// </summary>
    internal static DateTime? ToNullableDateTime(this Java.Util.Date? javaDate)
    {
        // convert Java.Util.Date to DateTime
        if (javaDate == null)
        {
            return null;
        }
        long milliseconds = javaDate.Time;
        DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        DateTime dateTime = epoch.AddMilliseconds(milliseconds);
        return dateTime.ToLocalTime();
    }
}
