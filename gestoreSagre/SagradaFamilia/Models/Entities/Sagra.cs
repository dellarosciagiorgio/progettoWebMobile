using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Sagra
    {
        public int IdSagra { get; set; }
        public string? NomeSagra { get; set; }
        public string? Descrizione { get; set; }
        public string? Luogo { get; set; }
        public List<Feedback> Feedbacks { get; set; } = new List<Feedback>();
        public List<Evento> Eventi { get; set; } = new List<Evento>();
        public Organizzatore Organizzatore { get; set; }
        public int IdOrganizzatore { get; set; }
    }
}
