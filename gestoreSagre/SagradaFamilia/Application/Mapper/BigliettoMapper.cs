using Application.Models.Dtos;
using Application.Models.Request;
using Models.Entities;

namespace Application.Mapper
{
    public class BigliettoMapper
    {
        public static List<Biglietto> ToEntity(List<AddBigliettoRequest> request)
        {
            List <Biglietto> biglietti = new List<Biglietto>();
            foreach (var item in request)
            {
                Biglietto biglietto = new Biglietto();
                biglietto.Nominativo = item.Nominativo;
                biglietto.IdTipoBiglietto = item.IdTipoBiglietto;
                biglietti.Add(biglietto);
            }
            return biglietti;
        }
        public static BigliettoDto ToDto(Biglietto biglietto)
        {
            throw new NotImplementedException();
        }
    }
}