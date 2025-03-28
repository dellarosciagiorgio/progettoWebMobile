using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstraction.Requests
{
    abstract public class BaseRequest
    {
        public int? IdUser { get; set; }
    }
}
