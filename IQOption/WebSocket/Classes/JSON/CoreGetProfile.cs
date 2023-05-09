using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IQOption.WebSocket.Classes.JSON
{
    public class CoreGetProfile
    {
        //public ? auth_two_factor;
        //public ? confirmation_required;
        //public ? currency;
        //public ? currency_char;
        //public ? currency_id;
        //public ? demo;
        public string email;
        //public ? flag;
        //public ? gender;
        public int group_id; //193
        //HINT - id and user_id is the same ???
        public int id;
        public int user_id;
        public string first_name;
        public string last_name;
        public string name;
        //public string skey;
    }
}