using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class StockBiglietto
    {
        public int IdStock { get; set; }
        public TipoBiglietto? TipoBiglietto { get; set; }
        public double Quantita { get; set; }
        public Evento? Evento { get; set; }
        public int IdEvento { get; set; }
        public int IdTipoBiglietto { get; set; }
    }
}
