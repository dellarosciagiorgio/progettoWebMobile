using Abstraction.Context;
using Application.Abstraction.Services;
using Application.Mapper;
using Application.Models.Request;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    internal class StockService : IStockService
    {
        private readonly IMyDbContext _context;
        //private readonly ILogger _logger;
        public StockService(
            //ILogger<AcquirenteService> logger,
            IMyDbContext context)
        {
            // _logger = logger;
            _context = context;
        }

        public async Task<StockBiglietto> AddStockAsync(AddStockRequest request)
        {
            var entity = StockMapper.ToEntity(request);
            TipoBiglietto tipoBiglietto = entity.TipoBiglietto;
            await _context.TipiBiglietto.AddAsync(tipoBiglietto);
            await _context.SaveChangesAsync();

            entity.IdTipoBiglietto = tipoBiglietto.IdTipo;
            var evento = await _context.Eventi.FindAsync(entity.IdEvento);
            var sagra = await _context.Sagre
                .Where(p => p.IdSagra == evento.IdSagra)
                .FirstOrDefaultAsync();
            if (sagra == null)
            {
                throw new Exception("Evento non trovato");
            }
            if (sagra.IdOrganizzatore != request.IdUser)
            {
                throw new Exception("L'organizzatore non è autorizzato a creare eventi per questa sagra");
            }
            await _context.Stocks.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteStockAsync(DeleteStockRequest request)
        {
            var stockToDelete = StockMapper.ToEntity(request);
            var evento = await _context.Eventi.FindAsync(stockToDelete.IdEvento);
            var sagra = await _context.Sagre
                .Where(p => p.IdSagra == evento.IdSagra)
                .FirstOrDefaultAsync();
            if (sagra == null)
            {
                throw new Exception("Evento non trovato");
            }
            if (sagra.IdOrganizzatore != request.IdUser)
            {
                throw new Exception("L'organizzatore non è autorizzato a creare eventi per questa sagra");
            }
            _context.Entry(stockToDelete).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<StockBiglietto> EditQuantitaStockAsync(EditQuantitaStockRequest request)
        {
            var entity = StockMapper.ToEntity(request);
            var evento = await _context.Eventi.FindAsync(entity.IdEvento);
            var sagra = await _context.Sagre
                .Where(p => p.IdSagra == evento.IdSagra)
                .FirstOrDefaultAsync();
            if (sagra == null)
            {
                throw new Exception("Evento non trovato");
            }
            if (sagra.IdOrganizzatore != request.IdUser)
            {
                throw new Exception("L'organizzatore non è autorizzato a creare eventi per questa sagra");
            }
            var entry = _context.Entry(entity);
            entry.Property(x => x.Quantita).IsModified = true;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<StockBiglietto>> GetStocksAsync()
        {
            return await _context.Stocks
                .Include(p => p.TipoBiglietto)
                .ToListAsync();
        }

        public async Task<List<StockBiglietto>> GetStocksByEventoAsync(int idEvento)
        {
            return await _context.Stocks
                .Where(x => x.IdEvento == idEvento)
                .Include(p => p.TipoBiglietto)
                .ToListAsync();
        }
    }
}
