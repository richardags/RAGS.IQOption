namespace IQOption.Interfaces
{
    public enum Status
    {
        Connecting,
        Connected,
        Closed,
        Error
    }

    public delegate void EventOnAuthenticationStatusEvent(Status status,
        string error_message = null);

    public interface IAuthentication
    {
        event EventOnAuthenticationStatusEvent OnAuthenticationStatusEvent;
    }
}