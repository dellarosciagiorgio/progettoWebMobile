using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicam.WebMobile.SagradaFamilia.Models.Entities
{
    public class Evento
    {
        public int IdEvento { get; set; }
        public Sagra Sagra { get; set; }
        public DateTime DataEvento { get; set; }
        public string InformazioniAggiuntive { get; set; }
        public List<StockBiglietto> StocksBiglietto { get; set; }

    }
}
