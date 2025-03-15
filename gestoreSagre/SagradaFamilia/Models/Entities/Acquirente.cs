namespace Models.Entities
{
    public class Acquirente
    {
        public int IdAcquirente { get; set; }
        public string Mail { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public List<Biglietto> BigliettiComprati { get; set; }
        public List<Feedback> Feedbacks { get; set; }

    }
}
