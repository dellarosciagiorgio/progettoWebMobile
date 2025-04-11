using Models.DetailedEntities;

namespace Models.Entities
{
    public class Admin : UserGenerico
    {
        public string? Nome { get; set; }
        public string? Cognome { get; set; }
    }
}