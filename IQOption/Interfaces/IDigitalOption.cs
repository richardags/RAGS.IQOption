using IQOption.WebSocket.Classes.JSON;

namespace IQOption.Interfaces
{
    public class Error
    {
        public int status;
        public string message;
    }

    public delegate void EventOnDigitalOptionPlaceDigitalOptionEvent(string request_id = null,
        long? order_id = null, Error error = null);
    public delegate void EventOnDigitalOptionSubscribePositionsEvent(PositionsState positionsState, string request_id = null);

    public interface IDigitalOption
    {
        event EventOnDigitalOptionPlaceDigitalOptionEvent OnDigitalOptionPlaceDigitalOptionEvent;
        event EventOnDigitalOptionSubscribePositionsEvent OnDigitalOptionSubscribePositionsEvent;
    }
}