using System.Collections.Generic;

namespace RAGS.IQOption.WebSocket.Classes.JSON
{
    public class DOIUnderlyingList
    {
        public Enumerations.InstrumentType type;
        public int user_group_id;
        public List<Underlying> underlying;
    }
}