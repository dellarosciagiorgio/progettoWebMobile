using Abstraction.Context;
using Application.Abstraction.Services;
using Application.Mapper;
using Application.Models.Request;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AcquirenteService : IAcquirenteService
    {
        private readonly IMyDbContext _context;
        //private readonly ILogger _logger;
        public AcquirenteService(
            //ILogger<AcquirenteService> logger,
            IMyDbContext context)
        {
           // _logger = logger;
            _context = context;
        }

        public async Task<Acquirente> AddAcquirenteAsync(AddAcquirenteRequest request)
        {
            var entity = AcquirenteMapper.ToEntity(request);
            await _context.Acquirenti.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Acquirente> EditAcquirenteAsync(EditAcquirenteRequest request)
        {
            var entity = AcquirenteMapper.ToEntity(request);
            var entry = _context.Entry<Acquirente>(entity);
            entry.Property(x => x.Mail).IsModified = true;
            entry.Property(x => x.Nome).IsModified = true;
            entry.Property(x => x.Cognome).IsModified = true;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<Acquirente>> GetAcquirentiAsync()
        {
            return await _context.Acquirenti.ToListAsync();
        }
    }
}
