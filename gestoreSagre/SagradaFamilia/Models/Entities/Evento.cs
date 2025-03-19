using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Evento
    {
        public int IdEvento { get; set; }
        public Sagra? Sagra { get; set; } = null;
        public DateTime DataEvento { get; set; }
        public string? InformazioniAggiuntive { get; set; }
        public List<StockBiglietto> StocksBiglietto { get; set; } = new List<StockBiglietto>();
        public int IdSagra { get; set; }
    }
}
