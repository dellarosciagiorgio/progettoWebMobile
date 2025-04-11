using Application.Models.Dtos;
using Application.Models.Requests;
using Models.Entities;

namespace Application.Mapper
{
    public class BigliettoMapper
    {
        public static List<Biglietto> ToEntity(AddBigliettoRequest request)
        {
            List <Biglietto> biglietti = new List<Biglietto>();
            foreach (var nominativo in request.Nominativo)
            {
                Biglietto biglietto = new Biglietto();
                biglietto.Nominativo = nominativo;

                biglietto.IdTipoBiglietto = request.IdTipoBiglietto;
                biglietto.IdAcquirente = (int)request.IdUser;
                biglietto.IdBiglietto = 0;
                /* 
                Acquirente acquirente = new Acquirente();
                acquirente.IdAcquirente = request.IdAcquirente;
                biglietto.Acquirente = acquirente;
                */

                /*
                TipoBiglietto tipoBiglietto = new TipoBiglietto();
                tipoBiglietto.IdTipo = request.IdTipoBiglietto;
                biglietto.TipoBiglietto = tipoBiglietto;
                */
                biglietti.Add(biglietto);
            }
            return biglietti;
        }
        public static BigliettoDto ToDto(Biglietto biglietto)
        {
            BigliettoDto bigliettoDto = new BigliettoDto();
            bigliettoDto.IdBiglietto = biglietto.IdBiglietto;
            bigliettoDto.Nominativo = biglietto.Nominativo;
            bigliettoDto.IdTipoBiglietto = biglietto.TipoBiglietto.IdTipo;
            bigliettoDto.IdAcquirente = biglietto.IdAcquirente;
            bigliettoDto.Prezzo = biglietto.TipoBiglietto.Prezzo;
            bigliettoDto.IdEvento = biglietto.TipoBiglietto.IdEvento;

            return bigliettoDto;
        }
    }
}