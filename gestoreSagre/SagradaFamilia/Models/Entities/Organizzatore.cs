using Models.DetailedEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Organizzatore : UserGenerico
    {
        public string? NomeOrganizzazione { get; set; }
        public List<Sagra> Sagre { get; set; } = new List<Sagra>();
    }
}
