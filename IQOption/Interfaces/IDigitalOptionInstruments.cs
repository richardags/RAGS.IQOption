using RAGS.IQOption.WebSocket.Classes.JSON;

namespace RAGS.IQOption.Interfaces
{
    public delegate void EventOnDigitalOptionInstrumentsEvent(string request_id,
        DOIGetInstruments doiGetInstruments, Error error = null);

    public interface IDigitalOptionInstruments
    {
        event EventOnDigitalOptionInstrumentsEvent OnDigitalOptionInstrumentsEvent;
    }
}