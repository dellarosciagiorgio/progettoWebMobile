using Application.Models.Dtos;
using Application.Models.Request;
using Models.Entities;

namespace Application.Mapper
{
    internal class StockMapper
    {
        public static StockBiglietto ToEntity(AddStockRequest request)
        {
            var stock = new StockBiglietto();
            stock.Quantita = request.Quantita;
            stock.IdEvento = request.IdEvento;

            var tipo = new TipoBiglietto();
            tipo.Descrizione = request.Descrizione;
            tipo.Tipo = request.Tipo;
            tipo.Prezzo = request.Prezzo;

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
            throw new NotImplementedException();
        }
    }
}