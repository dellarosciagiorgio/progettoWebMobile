using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicam.WebMobile.SagradaFamilia.Models.Entities
{
    public class Organizzatore
    {
        public int IdOrganizzatore { get; set; }
        public string Mail { get; set; }
        public string NomeOrganizzazione { get; set; }
        public List<Sagra> Sagre { get; set; }
    }
}
