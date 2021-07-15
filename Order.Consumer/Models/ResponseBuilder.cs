using System.Collections.Generic;
using System.Net;

namespace Order.Consumer.Models
{
    public class ResponseBuilder<T>
    {
        public Response<T> Build(T result)
        {
            return new Response<T>(HttpStatusCode.Accepted, result, false);
        }
        public Response<T> Build(T result, HttpStatusCode code, bool error, List<string> errors = null)
        {
            return new Response<T>(code, result, error, errors);
        }
    }
}
