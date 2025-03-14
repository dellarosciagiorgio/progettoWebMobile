namespace Models.Entities
{
    public class Biglietto
    {
        public int IdBiglietto { get; set; }
        public TipoBiglietto TipoBiglietto { get; set; }
        public string Nominativo { get; set; }
        public Acquirente Acquirente { get; set; }
    }
}