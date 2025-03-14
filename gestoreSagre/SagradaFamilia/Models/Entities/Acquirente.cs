using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Acquirente
    {
        public int IdAcquirente { get; set; }
        public string Mail { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public List<Biglietto> BigliettiComprati { get; set; }
        public List<Feedback> FeedBacks { get; set; }

    }
}
