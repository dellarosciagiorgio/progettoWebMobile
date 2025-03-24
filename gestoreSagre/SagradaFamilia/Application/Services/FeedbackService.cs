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
            await _context.Feedbacks.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
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
