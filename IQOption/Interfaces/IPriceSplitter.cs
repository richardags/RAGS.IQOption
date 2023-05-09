using IQOption.WebSocket.Classes.JSON;

namespace IQOption.Interfaces
{
    public delegate void EventOnPriceSplitterClientPriceGeneratedEvent(
        ClientPriceGenerated priceGenerated);

    public interface IPriceSplitter
    {
        event EventOnPriceSplitterClientPriceGeneratedEvent OnPriceSplitterClientPriceGeneratedEvent;
    }
}