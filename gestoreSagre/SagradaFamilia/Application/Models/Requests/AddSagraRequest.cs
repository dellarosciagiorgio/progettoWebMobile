namespace Application.Models.Request
{
    public class AddSagraRequest
    {
        public string NomeSagra { get; set; }
        public string Descrizione { get; set; }
        public int IdOrganizzatore { get; set; }
    }
}