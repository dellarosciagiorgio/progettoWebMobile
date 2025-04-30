using Application.Models.Dtos;
using Application.Models.Requests;
using Models.Entities;

namespace Application.Mapper
{
    public class BigliettoMapper
    {
        public static List<Biglietto> ToEntity(AddBigliettoRequest request)
        {
            List<Biglietto> biglietti = new List<Biglietto>();
            foreach (var nominativo in request.Nominativo)
            {
                Biglietto biglietto = new Biglietto();
                biglietto.Nominativo = nominativo;

                biglietto.IdTipoBiglietto = request.IdTipoBiglietto;
                biglietto.IdAcquirente = (int)request.IdUser;
                biglietto.IdBiglietto = 0;

                biglietti.Add(biglietto);
            }
            return biglietti;
        }

        public static BigliettoDto ToDto(Biglietto biglietto)
        {
            BigliettoDto bigliettoDto = new BigliettoDto();
            bigliettoDto.IdBiglietto = biglietto.IdBiglietto;
            bigliettoDto.Nominativo = biglietto.Nominativo;
            bigliettoDto.IdTipoBiglietto = biglietto.IdTipoBiglietto;
            bigliettoDto.IdAcquirente = biglietto.IdAcquirente;

            bigliettoDto.Prezzo = biglietto.TipoBiglietto?.Prezzo ?? 0;

            return bigliettoDto;
        }
    }
}