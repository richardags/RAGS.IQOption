using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAGS.IQOption.WebSocket.Classes.JSON
{
    public class PositionsState
    {
        public int user_id;
        public List<PositionStatePosition> positions;
        //public int expires_in; //remanining 60s
        //public ulong subscription_id; //7,024,612,092,650,965,00        
    }
}