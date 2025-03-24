using Application.Models.Request;
using Application.Models.Requests;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstraction.Services
{
    public interface IFeedbackService
    {
        Task<List<Feedback>> GetFeedbacksAsync();
        Task<List<Feedback>> GetFeedbacksByAcquirenteAsync(GetFeedbackByAcquirenteRequest request);
        Task<List<Feedback>> GetFeedbacksBySagraAsync(GetFeedbackBySagraRequest request);
        Task<Feedback> AddFeedbackAsync(AddFeedbackRequest request);
        Task<Feedback> EditFeedbackAsync(EditFeedbackRequest request);
        Task DeleteFeedback(DeleteFeedbackRequest request);
    }
}
