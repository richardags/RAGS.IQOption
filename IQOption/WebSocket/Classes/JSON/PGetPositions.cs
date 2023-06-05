using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAGS.IQOption.WebSocket.Classes.JSON
{
    public class PGetPositions
    {
        public List<Position> positions;
        public int total;
        public int limit;
    }
}