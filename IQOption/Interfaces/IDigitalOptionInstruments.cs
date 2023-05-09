using IQOption.WebSocket.Classes.JSON;

namespace IQOption.Interfaces
{
    public delegate void EventOnDigitalOptionInstrumentsEvent(string request_id,
        DOIGetInstruments doiGetInstruments, Error error = null);

    public interface IDigitalOptionInstruments
    {
        event EventOnDigitalOptionInstrumentsEvent OnDigitalOptionInstrumentsEvent;
    }
}