using Abstraction.Context;
using Application.Abstraction.Services;
using Application.Mapper;
using Application.Models.Requests;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    internal class BigliettoService : IBigliettoService
    {
        private readonly IMyDbContext _context;
        //private readonly ILogger _logger;
        public BigliettoService(
            //ILogger<AcquirenteService> logger,
            IMyDbContext context)
        {
            // _logger = logger;
            _context = context;
        }
        public async Task<List<Biglietto>> AddBigliettiAsync(AddBigliettoRequest request)
        {
            List<Biglietto> entity = BigliettoMapper.ToEntity(request);
            var stock = await _context.Stocks
                .Where(x => x.IdTipoBiglietto == request.IdTipoBiglietto)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            if(stock == null)
            {
                throw new Exception("Tipo Biglietto non trovato");
            }
            if (stock.Quantita < entity.Count)
            {
                throw new Exception($"Quantità disponibile non sufficiente per acquistare {entity.Count} biglietti");
            }
            foreach (var item in entity)
            {
                await _context.Biglietti.AddAsync(item);
            }
            IStockService stockService = new StockService(_context);
            
            await stockService.EditQuantitaStockAsync(
                new EditQuantitaStockRequest
                {
                    IdStock = stock.IdStock,
                    Quantita = stock.Quantita - entity.Count
                });

            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<Biglietto>> GetBigliettiByAcquirenteAsync(int idAcquirente)
        {
            return await _context.Biglietti
                .Where(x => x.IdAcquirente == idAcquirente)
                .Include(x => x.TipoBiglietto!)
                //.ThenInclude(tb => tb.Evento!)
                .Select(selector => new Biglietto
                {
                    IdBiglietto = selector.IdBiglietto,
                    IdAcquirente = selector.IdAcquirente,
                    Nominativo = selector.Nominativo,
                    TipoBiglietto = new TipoBiglietto
                    {
                        IdTipo = selector.TipoBiglietto!.IdTipo,
                        Prezzo = selector.TipoBiglietto.Prezzo,
                        IdEvento = selector.TipoBiglietto.IdEvento,
                    }
                })
                .ToListAsync();
        }
    }
}
