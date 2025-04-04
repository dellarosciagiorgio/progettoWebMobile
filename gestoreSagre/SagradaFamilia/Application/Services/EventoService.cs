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
            var sagra = await _context.Sagre.FindAsync(request.IdSagra);
            if (sagra == null)
            {
                throw new Exception("Sagra non trovata");
            }
            if(sagra.IdOrganizzatore != request.IdUser)
            {
                throw new Exception("L'organizzatore non è autorizzato a creare eventi per questa sagra");
            }
            //la data dell'evento non può essere antecedente alla data successiva alla data odierna 
            if (request.DataEvento < DateTime.Now.AddDays(1))
            {
                throw new Exception("La data dell'evento non può essere antecedente alla data successiva alla data odierna");
            }
            var entity = EventoMapper.ToEntity(request);
            await _context.Eventi.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteEventoAsync(DeleteEventoRequest request)
        {
            var evento = await _context.Eventi.FindAsync(request.IdEvento);
            if (evento == null)
            {
                throw new Exception("Evento non trovato");
            }
            var sagra = await _context.Sagre.FindAsync(evento.IdSagra);
            if (sagra == null)
            {
                throw new Exception("Sagra non trovata");
            }
            if (sagra.IdOrganizzatore != request.IdUser)
            {
                throw new Exception("L'organizzatore non è autorizzato a cancellare eventi per questa sagra");
            }
            var eventoToDelete = EventoMapper.ToEntity(request);
            _context.Entry(eventoToDelete).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<Evento> EditEventoAsync(EditEventoRequest request)
        {

            var evento = EventoMapper.ToEntity(request);
            if (evento == null)
            {
                throw new Exception("Evento non trovato");
            }
            var sagra = await _context.Sagre.FindAsync(evento.IdSagra);
            if (sagra == null)
            {
                throw new Exception("Sagra non trovata");
            }
            if (sagra.IdOrganizzatore != request.IdUser)
            {
                throw new Exception("L'organizzatore non è autorizzato a cancellare eventi per questa sagra");
            }
            var entry = _context.Entry<Evento>(evento);
            entry.Property(x => x.InformazioniAggiuntive).IsModified = true;
            entry.Property(x => x.DataEvento).IsModified = true;
            await _context.SaveChangesAsync();
            return evento;
        }

        public async Task<List<Evento>> GetEventiAsync()
        {
            return await _context.Eventi.ToListAsync();
        }

        public async Task<List<Evento>> GetEventiBySagraAsync(int idSagra)
        {
            if (idSagra < 1)
            {
                throw new Exception("Id sagra non valido");
            }
            var list =  await _context.Eventi
                .Where(x => x.IdSagra == idSagra)
                .ToListAsync();
            if(list.Count == 0)
            {
                throw new Exception("Nessun evento trovato per questa sagra");
            }
            return list;
        }

        public async Task<List<Evento>> GetEventiAsync(int idUser, Ruolo ruolo, bool eventiFuturi)
        {
            DateTime now = DateTime.Now;
            switch (ruolo)
            {
                case Ruolo.Acquirente:
                    var myIdEvento = await _context.Biglietti
                        .Where(x => x.IdAcquirente == idUser && x.TipoBiglietto != null)
                        .Select(x => x.TipoBiglietto!.IdEvento)
                        .ToListAsync();

                    return await _context.Eventi
                        .Where(x => myIdEvento.Contains(x.IdEvento) &&
                                    (eventiFuturi ? x.DataEvento > now : x.DataEvento <= now))
                        .ToListAsync();
            
                case Ruolo.Organizzatore:
                
                    return await _context.Eventi
                        .Where(x => x.Sagra != null && x.Sagra.IdOrganizzatore == idUser &&
                                    (eventiFuturi ? x.DataEvento > now : x.DataEvento <= now))
                        .ToListAsync();

                default: 
                    throw new Exception("Ruolo non valido");
            }
        }

        public async Task<List<Evento>> GetEventiAsync(bool eventiFuturi)
        {
            DateTime now = DateTime.Now;
            return await _context.Eventi
                .Where(x => eventiFuturi ? x.DataEvento > now : x.DataEvento <= now)
                .ToListAsync();
        }
    }
}
