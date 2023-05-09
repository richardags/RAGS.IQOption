using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQOption.WebSocket.Classes.JSON
{
    public class Commissions
    {
        public class Item
        {
            public int active_id;
            //public ? open_fixed;
            //public ? open_percent;
        }

        public Enumerations.InstrumentType instrument_type;
        public int user_group_id;
        public List<Item> items;
    }
}