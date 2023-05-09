using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQOption.WebSocket.Classes.JSON
{
    public class CommissionChanged
    {
        public class Commission {
            public int value;
        }

        public Enumerations.InstrumentType instrument_type;
        public int user_group_id;
        public int active_id;
        public Commission commission;
    }
}