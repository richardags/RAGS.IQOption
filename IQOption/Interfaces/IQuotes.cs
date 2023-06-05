using RAGS.IQOption.WebSocket.Classes.JSON;
using System.Collections.Generic;

namespace RAGS.IQOption.Interfaces
{
    public delegate void EventOnQuotesGetCandlesEvent(string request_id, List<Candle> candles);
    public delegate void EventOnQuotesCandleGeneratedEvent(CandleGenerated candleGenerated);

    public interface IQuotes
    {
        event EventOnQuotesGetCandlesEvent OnQuotesGetCandlesEvent;
        event EventOnQuotesCandleGeneratedEvent OnQuotesCandleGenerated;
    }
}