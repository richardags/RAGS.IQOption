using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RAGS.IQOption.WebRequest
{
    internal class RequestError
    {
        internal HttpStatusCode code;
        internal string message = "";

        internal RequestError(string message)
        {
            this.message = message;
        }
        internal RequestError(HttpStatusCode code)
        {
            this.code = code;
        }
        internal RequestError(HttpStatusCode code, string message)
        {
            this.code = code;
            this.message = message;
        }
    }
}