using System.Collections.Generic;
using System.Net;

namespace Order.Consumer.Models
{
    public class Response<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public T Result { get; set; }
        public bool Error { get; set; }
        public List<string> Errors { get; set; } = new List<string>();

        public Response(HttpStatusCode status, T result, bool error, List<string> errors = null)
        {
            StatusCode = status;
            Result = result;
            Error = error;
            Errors = errors;
        }
    }
}
