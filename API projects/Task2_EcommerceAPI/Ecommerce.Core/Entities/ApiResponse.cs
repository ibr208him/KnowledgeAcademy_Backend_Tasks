using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Entities
{
    public class ApiResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public Boolean IsSuccess { get; set; }
        public String ErrorMessages { get; set; }
        public object Result { get; set; }
    }
}
