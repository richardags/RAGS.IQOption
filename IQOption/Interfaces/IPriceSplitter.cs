using RAGS.IQOption.WebSocket.Classes.JSON;

namespace RAGS.IQOption.Interfaces
{
    public delegate void EventOnPriceSplitterClientPriceGeneratedEvent(
        ClientPriceGenerated priceGenerated);

    public interface IPriceSplitter
    {
        event EventOnPriceSplitterClientPriceGeneratedEvent OnPriceSplitterClientPriceGeneratedEvent;
    }
}