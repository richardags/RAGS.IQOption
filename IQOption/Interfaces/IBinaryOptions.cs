using IQOption.WebSocket.Classes.JSON;
namespace IQOption.Interfaces
{
    public class BinaryOptionsOpenOptionException
    {

        public const int INSUFFICIENT_FUNDS = 4100;
        public const int EXPIRATION_OUT_OF_SCHEDULE = 4104;
        public const int REQUEST_LIMIT_REACHED = 4121;

        public int Code { get; private set; }
        public string Message { get; private set; }

        public BinaryOptionsOpenOptionException(int Code, string Message)
        {
            this.Message = Message;
            this.Code = Code;
        }
    }

    public delegate void EventOnBinaryOptionsOpenOptionEvent(string request_id = null,
        Option option = null, BinaryOptionsOpenOptionException error = null);

    public interface IBinaryOptions
    {
        event EventOnBinaryOptionsOpenOptionEvent OnBinaryOptionsOpenOptionEvent;
    }
}