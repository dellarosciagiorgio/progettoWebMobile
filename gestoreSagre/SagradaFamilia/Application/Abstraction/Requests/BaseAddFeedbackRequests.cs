using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstraction.Requests;

namespace Application.Abstraction.Requests
{
    abstract public class BaseAddFeedbackrequests : BaseRequest
    {
        public string Titolo { get; set; }
        public string Descrizione { get; set; }
        public int IdFeedback { get; set; }
        public int Rating { get; set; }
    }
}
