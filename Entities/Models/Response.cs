using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Response
    {
        public int StatusCode { get; set; }
        public bool Success { get; set; }
        public object Data { get; set; }

        public Response(int statusCode, bool success, object data)
        {
            StatusCode = statusCode;
            Success = success;
            Data = data;
        }
    }
}
