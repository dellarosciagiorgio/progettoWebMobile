using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicam.WebMobile.SagradaFamilia.Models.Entities
{
    public class Sagra
    {
        public int IdSagra { get; set; }
        public string NomeSagra { get; set; }
        public string Descrizione { get; set; }
        public List<Feedback> Feedbacks { get; set; }
        public List<Evento> Eventi { get; set; }
        public Organizzatore Organizzatore { get; set; }
    }
}
