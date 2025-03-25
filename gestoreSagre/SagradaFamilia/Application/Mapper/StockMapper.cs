using Application.Models.Dtos;
using Application.Models.Request;
using Models.Entities;

namespace Application.Mapper
{
    public class StockMapper
    {
        public static StockBiglietto ToEntity(AddStockRequest request)
        {
            var stock = new StockBiglietto();
            stock.Quantita = request.Quantita;
            stock.IdEvento = request.IdEventoEsistente;

            var tipo = new TipoBiglietto();
            tipo.Prezzo = request.Prezzo;
            tipo.Descrizione = request.Descrizione;
            tipo.IdEvento = request.IdEventoEsistente;
            tipo.Tipo = request.Tipo;

            stock.TipoBiglietto = tipo;
            return stock;
        }

        public static StockBiglietto ToEntity(DeleteStockRequest request)
        {
            var stock = new StockBiglietto();
            stock.IdStock = request.IdStock;
            return stock;
        }

        public static StockBiglietto ToEntity(EditQuantitaStockRequest request)
        {
            var stock = new StockBiglietto();
            stock.IdStock = request.IdStock;
            stock.Quantita = request.Quantita;
            return stock;
        }

        public static StockDto ToDto(StockBiglietto stock)
        {
            StockDto stockDto = new StockDto();
            stockDto.IdStock = stock.IdStock;
            stockDto.IdTipoBiglietto = stock.IdTipoBiglietto;
            stockDto.Prezzo = stock.TipoBiglietto.Prezzo;
            stockDto.Descrizione = stock.TipoBiglietto.Descrizione;
            stockDto.Tipo = stock.TipoBiglietto.Tipo;
            stockDto.IdEvento = stock.TipoBiglietto.IdEvento;
            stockDto.Quantita = stock.Quantita;
            return stockDto;
        }
    }
}