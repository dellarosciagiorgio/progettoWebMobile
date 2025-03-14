using Models.Entities;

namespace Application.Models.Dtos
{
    public class EventoDto
    {
        public int IdEvento { get; set; }
        public int IdSagra { get; set; }
        public DateTime DataEvento { get; set; }
        public string InformazioniAggiuntive { get; set; }
        public List<int> IdStocksBiglietto { get; set; }
    }
}