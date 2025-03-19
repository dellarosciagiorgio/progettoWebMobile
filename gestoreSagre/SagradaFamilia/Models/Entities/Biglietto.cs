namespace Models.Entities
{
    public class Biglietto
    {
        public int IdBiglietto { get; set; }
        public TipoBiglietto? TipoBiglietto { get; set; } = null;
        public string? Nominativo { get; set; }
        public Acquirente? Acquirente { get; set; } = null;
        public int IdTipoBiglietto { get; set; }
    }
}