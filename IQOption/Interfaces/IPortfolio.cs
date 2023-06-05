using RAGS.IQOption.WebSocket.Classes.JSON;

namespace RAGS.IQOption.Interfaces
{
    public delegate void EventOnPortfolioGetPositionsEvent(string request_id,
        PGetPositions pGetPositions);
    public delegate void EventOnPortfolioPositionChangedEvent(Position position);
    public delegate void EventOnPortfolioOrderChangedEvent(Position position);

    public interface IPortfolio
    {
        event EventOnPortfolioGetPositionsEvent OnPortfolioGetPositionsEvent;
        event EventOnPortfolioPositionChangedEvent OnPortfolioPositionChangedEvent;
        event EventOnPortfolioOrderChangedEvent OnPortfolioOrderChangedEvent;
    }
}