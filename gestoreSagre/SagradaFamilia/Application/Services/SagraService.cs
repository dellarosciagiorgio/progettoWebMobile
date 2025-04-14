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
    public class SagraService : ISagraService
    {
        private readonly IMyDbContext _context;
        //private readonly ILogger _logger;
        public SagraService(
            //ILogger<AcquirenteService> logger,
            IMyDbContext context)
        {
            // _logger = logger;
            _context = context;
        }
        public async Task<Sagra> AddSagraAsync(AddSagraRequest request)
        {
            var sagra = SagraMapper.ToEntity(request);
            if (sagra == null)
            {
                throw new Exception("Sagra non valida");
            }
            if (sagra.IdOrganizzatore != request.IdUser)
            {
                throw new Exception("L'organizzatore non è autorizzato a creare eventi per questa sagra");
            }
            await _context.Sagre.AddAsync(sagra);
            await _context.SaveChangesAsync();
            return sagra;
        }

        public async Task DeleteSagraAsync(DeleteSagraRequest request)
        {

            var sagraToDelete = SagraMapper.ToEntity(request);
            if (sagraToDelete == null)
            {
                throw new Exception("Sagra non valida");
            }
            if (sagraToDelete.IdOrganizzatore != request.IdUser)
            {
                throw new Exception("L'organizzatore non è autorizzato a creare eventi per questa sagra");
            }
            var sagraCheck = await _context.Sagre.FindAsync(sagraToDelete.IdSagra);
            if (sagraCheck == null)
            {
                throw new Exception("Sagra non trovata");
            }
            if (sagraCheck.IdOrganizzatore != request.IdUser)
            {
                throw new Exception("L'organizzatore non è autorizzato a modificare questa sagra");
            }
            _context.Entry(sagraToDelete).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<Sagra> EditSagraAsync(EditSagraRequest request)
        {
            var sagra = SagraMapper.ToEntity(request);
            if (sagra == null)
            {
                throw new Exception("Sagra non valida");
            }
            if (sagra.IdOrganizzatore != request.IdUser)
            {
                throw new Exception("L'organizzatore non è autorizzato a creare eventi per questa sagra");
            }
            var sagraCheck = await _context.Sagre.FindAsync(sagra.IdSagra);
            if (sagraCheck == null)
            {
                throw new Exception("Sagra non trovata");
            }
            if(sagraCheck.IdOrganizzatore != request.IdUser)
            {
                throw new Exception("L'organizzatore non è autorizzato a modificare questa sagra");
            }
            var entry = _context.Entry<Sagra>(sagra);
            entry.Property(x => x.Descrizione).IsModified = true;
            await _context.SaveChangesAsync();
            return sagra;
        }

        public async Task<List<Sagra>> GetSagreAsync()
        {
            return await _context.Sagre
                .Include(s => s.Feedbacks)
                .ToListAsync();
        }

        public async Task<Sagra> GetSagreAsync(int id)
        {
            return await _context.Sagre
                .Where(p => p.IdSagra == id)
                .Include(s => s.Feedbacks)
                .FirstAsync();
        }
        public async Task<double> GetRatingSagraAsync(int id)
        {
            var sagra = await _context.Sagre
                .Where(p => p.IdSagra == id)
                .Include(s => s.Feedbacks)
                .FirstAsync();
            return sagra.Feedbacks
                .Where(f => f != null && f.Rating > 0)
                .Average(f => f.Rating);
        }
    }
}
