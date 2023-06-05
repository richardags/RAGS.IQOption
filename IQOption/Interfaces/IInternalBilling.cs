using RAGS.IQOption.WebSocket.Classes.JSON;

namespace RAGS.IQOption.Interfaces
{
    public delegate void EventOnInternalBillingBalanceChangedEvent(BalanceChanged balanceChanged);

    public interface IInternalBilling
    {
        event EventOnInternalBillingBalanceChangedEvent OnInternalBillingBalanceChangedEvent;
    }
}