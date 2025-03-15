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
    public class EventoService : IEventoService
    {
        private readonly IMyDbContext _context;
        //private readonly ILogger _logger;
        public EventoService(
            //ILogger<AcquirenteService> logger,
            IMyDbContext context)
        {
            // _logger = logger;
            _context = context;
        }
        public async Task<Evento> AddEventoAsync(AddEventoRequest request)
        {
            var entity = EventoMapper.ToEntity(request);
            await _context.Eventi.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteEventoAsync(DeleteEventoRequest request)
        {
            var eventoToDelete = EventoMapper.ToEntity(request);
            _context.Entry(eventoToDelete).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<Evento> EditEventoAsync(EditEventoRequest request)
        {
            var entity = EventoMapper.ToEntity(request);
            var entry = _context.Entry<Evento>(entity);
            entry.Property(x => x.InformazioniAggiuntive).IsModified = true;
            entry.Property(x => x.DataEvento).IsModified = true;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<Evento>> GetEventiAsync()
        {
            return await _context.Eventi.ToListAsync();
        }

        public async Task<List<Evento>> GetEventiBySagraAsync(int idSagra)
        {
            return await _context.Eventi
                .Where(x => x.IdSagra == idSagra)
                .ToListAsync();
        }
    }
}
