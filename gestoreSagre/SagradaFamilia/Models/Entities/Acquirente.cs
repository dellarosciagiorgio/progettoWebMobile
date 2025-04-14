using Models.DetailedEntities;

namespace Models.Entities
{
    public class Acquirente : UserGenerico
    {
        public string? Nome { get; set; }
        public string? Cognome { get; set; }
        public List<Biglietto> BigliettiComprati { get; set; } = new List<Biglietto>();
        public List<Feedback> Feedbacks { get; set; } = new List<Feedback>();
    }
}