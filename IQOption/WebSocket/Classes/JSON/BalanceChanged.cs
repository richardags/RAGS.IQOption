using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAGS.IQOption.WebSocket.Classes.JSON
{
    public class BalanceChanged
    {
        public class CurrentBalance
        {
            public long id;
            public double amount;
            public int type;
        }

        public CurrentBalance current_balance;
        public long id;
        public long user_id;
    }
}