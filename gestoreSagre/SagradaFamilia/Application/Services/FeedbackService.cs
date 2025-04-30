using Abstraction.Context;
using Application.Abstraction.Requests;
using Application.Abstraction.Services;
using Application.Mapper;
using Application.Models.Requests;
using Application.Models.Requests;
using Microsoft.EntityFrameworkCore;
using Models.DetailedEntities;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IMyDbContext _context;
        //private readonly ILogger _logger;
        public FeedbackService(
            //ILogger<AcquirenteService> logger,
            IMyDbContext context)
        {
            // _logger = logger;
            _context = context;
        }

        public async Task<Feedback> AddFeedbackBySagraAsync(AddFeedbackBySagraRequest request)
        {
            var entity = FeedbackMapper.ToEntity(request);

            return await AddFeedback(entity);
        }

        public async Task<Feedback> AddFeedbackByEventoAsync(AddFeedbackByEventoRequest request)
        {
            var entity = FeedbackMapper.ToEntity(request);

            Sagra sagra = await _context.Sagre
                .Include(x => x.Eventi)
                .FirstOrDefaultAsync(x => x.Eventi.Any(e => e.IdEvento == request.IdEvento));
            if(sagra == null)
            {
                throw new Exception("Sagra non trovata");
            }
            entity.IdSagra = sagra.IdSagra;
            return await AddFeedback(entity);
        }



        private async Task<Feedback> AddFeedback( Feedback entity )
        {
            var eventiSagra = await _context.Sagre
             .Where(x => x.IdSagra == entity.IdSagra)
             .Select(x => x.Eventi)
             .FirstOrDefaultAsync();

            var myTipoBiglietto = await _context.Biglietti
               .Where(x => x.IdAcquirente == entity.IdAcquirente && x.TipoBiglietto != null)
               .Select(x => x.TipoBiglietto!.IdTipo)
               .ToListAsync();
            var myIdEvento = await _context.Stocks
                .Where(x => myTipoBiglietto.Contains(x.IdTipoBiglietto))
                .Select(x => x.IdEvento)
                .ToListAsync();

            var myeventi = await _context.Eventi
                .Where(x => myIdEvento.Contains(x.IdEvento))
                .ToListAsync();

            //list di eventi a cui l'utente ha partecipato/parteciperà e che sono comuni con la sagra
            var eventiComuni = eventiSagra.Intersect(myeventi).ToList();

            if (eventiComuni.Count == 0)
            {
                throw new Exception("L'acquirente non ha partecipato a nessun evento della sagra");
            }

            foreach (var evento in eventiComuni)
            {
                var feedback = await _context.Feedbacks
                    .Where(x => x.IdAcquirente == entity.IdAcquirente && x.IdSagra == entity.IdSagra)
                    .FirstOrDefaultAsync();
                if (feedback != null)
                {
                    throw new Exception("L'acquirente ha già lasciato un feedback per questa sagra");
                }
            }

            foreach (var evento in eventiComuni)
            {
                if (evento.DataEvento < DateTime.Now)

                {
                    await _context.Feedbacks.AddAsync(entity);
                    await _context.SaveChangesAsync();
                    return entity;
                }
            }
            throw new Exception("L'acquirente non ha ancora partecipato all'evento, non può quindi lasciare un feedback");

        }

        public async Task<Feedback> EditFeedbackAsync(EditFeedbackRequest request)
        {
            var entity = FeedbackMapper.ToEntity(request);
            var entry = _context.Entry<Feedback>(entity);
            entry.Property(x => x.Descrizione).IsModified = true;
            entry.Property(x => x.Titolo).IsModified = true;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteFeedback(DeleteFeedbackRequest request)
        {
            var feedbackToDelete = FeedbackMapper.ToEntity(request);
            _context.Entry(feedbackToDelete).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Feedback>> GetFeedbacksAsync()
        {
            return await _context.Feedbacks.ToListAsync();
        }

        public async Task<List<Feedback>> GetFeedbacksByAcquirenteAsync(SomethingByUserRequest request)
        {
            return await _context.Feedbacks
                .Where(x => x.IdAcquirente == request.IdUser)
                .ToListAsync();
        }

        public async Task<List<Feedback>> GetFeedbacksBySagraAsync(GetFeedbackBySagraRequest request)
        {
            return await _context.Feedbacks
                .Where(x => x.IdSagra == request.IdSagra)
                .ToListAsync();
        }


    }
}
