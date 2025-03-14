using Abstraction.Context;
using Application.Abstraction.Services;
using Application.Mapper;
using Application.Request;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    internal class SagraService : ISagraService
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
            var entity = SagraMapper.ToEntity(request);
            await _context.Sagre.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteSagraAsync(DeleteSagraRequest request)
        {
            var sagraToDelete = SagraMapper.ToEntity(request);
            _context.Entry(sagraToDelete).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<Sagra> EditSagraAsync(EditSagreRequest request)
        {
            var entity = SagraMapper.ToEntity(request);
            var entry = _context.Entry<Sagra>(entity);
            entry.Property(x => x.Descrizione).IsModified = true;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<Sagra>> GetSagreAsync()
        {
            {
                return await _context.Sagre.ToListAsync();
            }
        }
    }
}
