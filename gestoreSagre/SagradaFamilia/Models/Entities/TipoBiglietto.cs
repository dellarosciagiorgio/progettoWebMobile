using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class TipoBiglietto
    {
        public int IdTipo { get; set; }
        public string Tipo { get; set; }
        public double Prezzo { get; set; }
        public string Descrizione { get; set; }
        public Evento Evento { get; set; }
        
    }
}
