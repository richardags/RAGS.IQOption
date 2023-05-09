using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IQOption.WebRequest
{
    internal class RequestResult
    {
        internal string data;
        internal HttpStatusCode code;
        internal RequestError error;

        internal RequestResult(string data, HttpStatusCode code)
        {
            this.data = data;
            this.code = code;
        }
        internal RequestResult(RequestError error)
        {
            this.error = error;
        }
    }
}