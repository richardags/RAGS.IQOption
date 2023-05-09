using System.Collections.Generic;

namespace IQOption.WebSocket.Classes.JSON
{
    public class DOIUnderlyingList
    {
        public Enumerations.InstrumentType type;
        public int user_group_id;
        public List<Underlying> underlying;
    }
}