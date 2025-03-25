using Abstraction.Context;
using Application.Abstraction.Services;
using Application.Mapper;
using Application.Models.Request;
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
 
        public async Task<Feedback> AddFeedbackAsync(AddFeedbackRequest request)
        {
            var entity = FeedbackMapper.ToEntity(request);

            var eventi2 = await _context.Sagre
                 .Where(x => x.IdSagra == entity.IdSagra)
                 .Select(x => x.Eventi)
                 .FirstOrDefaultAsync();
            var eventi = await _context.Biglietti
                .Where(x => x.IdAcquirente == entity.IdAcquirente)
                .Include(x => x.TipoBiglietto)
                .Select(x => x.TipoBiglietto.Evento)
                .ToListAsync();

            //list di eventi a cui l'utente ha partecipato/parteciperà e che sono comuni con la sagra
            var eventiComuni = eventi2.Intersect(eventi).ToList();

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
                if(evento.DataEvento < DateTime.Now)
                
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

        public async Task<List<Feedback>> GetFeedbacksByAcquirenteAsync(GetFeedbackByAcquirenteRequest request)
        {
            return await _context.Feedbacks
                .Where(x => x.IdAcquirente == request.IdAcquirente)
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
