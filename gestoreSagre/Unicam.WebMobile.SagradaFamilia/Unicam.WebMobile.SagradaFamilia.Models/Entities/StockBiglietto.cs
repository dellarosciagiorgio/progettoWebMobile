using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicam.WebMobile.SagradaFamilia.Models.Entities
{
    public class StockBiglietto
    {
        public int IdStock { get; set; }
        public TipoBiglietto TipoBiglietto { get; set; }
        public double Quantita { get; set; }
    }
}
