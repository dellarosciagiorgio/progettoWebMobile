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
            foreach (var item in entity)
            {
                await _context.Biglietti.AddAsync(item);
            }

            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<Biglietto>> GetBigliettiByAcquirenteAsync(int idAcquirente)
        {
            return await _context.Biglietti
                .Where(x => x.IdAcquirente == idAcquirente)
                .ToListAsync();
        }
    }
}
