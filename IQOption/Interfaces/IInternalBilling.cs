using IQOption.WebSocket.Classes.JSON;

namespace IQOption.Interfaces
{
    public delegate void EventOnInternalBillingBalanceChangedEvent(BalanceChanged balanceChanged);

    public interface IInternalBilling
    {
        event EventOnInternalBillingBalanceChangedEvent OnInternalBillingBalanceChangedEvent;
    }
}