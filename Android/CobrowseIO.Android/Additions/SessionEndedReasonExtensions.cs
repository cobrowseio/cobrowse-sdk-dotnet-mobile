namespace Cobrowse.IO.Android;

internal static class SessionEndedReasonExtensions
{
    internal static Cobrowse.IO.Android.Session.EndedReasonJava ToJavaEnum(
        this Cobrowse.IO.Android.SessionEndedReason reason)
    {
        switch (reason)
        {
            case Cobrowse.IO.Android.SessionEndedReason.Unknown:
                return Cobrowse.IO.Android.Session.EndedReasonJava.Unknown;
            case Cobrowse.IO.Android.SessionEndedReason.DeviceEnded:
                return Cobrowse.IO.Android.Session.EndedReasonJava.DeviceEnded;
            case Cobrowse.IO.Android.SessionEndedReason.AgentEnded:
                return Cobrowse.IO.Android.Session.EndedReasonJava.AgentEnded;
            case Cobrowse.IO.Android.SessionEndedReason.PendingTimeout:
                return Cobrowse.IO.Android.Session.EndedReasonJava.PendingTimeout;
            case Cobrowse.IO.Android.SessionEndedReason.AuthorizingTimeout:
                return Cobrowse.IO.Android.Session.EndedReasonJava.AuthorizingTimeout;
            case Cobrowse.IO.Android.SessionEndedReason.ActiveTimeout:
                return Cobrowse.IO.Android.Session.EndedReasonJava.ActiveTimeout;
            case Cobrowse.IO.Android.SessionEndedReason.LimitEnforcement:
                return Cobrowse.IO.Android.Session.EndedReasonJava.LimitEnforcement;
            default:
                return default;
        }
    }

    internal static Cobrowse.IO.Android.SessionEndedReason ToManagedEnum(
        this Cobrowse.IO.Android.Session.EndedReasonJava javaReason)
    {
        if (javaReason == Cobrowse.IO.Android.Session.EndedReasonJava.Unknown)
        {
            return Cobrowse.IO.Android.SessionEndedReason.Unknown;
        }
        if (javaReason == Cobrowse.IO.Android.Session.EndedReasonJava.DeviceEnded)
        {
            return Cobrowse.IO.Android.SessionEndedReason.DeviceEnded;
        }
        if (javaReason == Cobrowse.IO.Android.Session.EndedReasonJava.AgentEnded)
        {
            return Cobrowse.IO.Android.SessionEndedReason.AgentEnded;
        }
        if (javaReason == Cobrowse.IO.Android.Session.EndedReasonJava.PendingTimeout)
        {
            return Cobrowse.IO.Android.SessionEndedReason.PendingTimeout;
        }
        if (javaReason == Cobrowse.IO.Android.Session.EndedReasonJava.AuthorizingTimeout)
        {
            return Cobrowse.IO.Android.SessionEndedReason.AuthorizingTimeout;
        }
        if (javaReason == Cobrowse.IO.Android.Session.EndedReasonJava.ActiveTimeout)
        {
            return Cobrowse.IO.Android.SessionEndedReason.ActiveTimeout;
        }
        if (javaReason == Cobrowse.IO.Android.Session.EndedReasonJava.LimitEnforcement)
        {
            return Cobrowse.IO.Android.SessionEndedReason.LimitEnforcement;
        }
        return default;
    }
}
