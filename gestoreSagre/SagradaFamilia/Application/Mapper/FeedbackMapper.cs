using Application.Abstraction.Services;
using Application.Models.Dtos;
using Application.Models.Request;
using Application.Models.Requests;
using Models.Entities;

namespace Application.Mapper
{
    public class FeedbackMapper 
    {
        public static Feedback ToEntity(AddFeedbackRequest request)
        {
            var entity = new Feedback();
            entity.IdAcquirente = (int)request.IdUser;
            entity.Titolo = request.Titolo;
            entity.Descrizione = request.Descrizione;
            entity.IdSagra = request.IdSagra;
            entity.Rating = request.Rating;
            return entity;
        }

        public static Feedback ToEntity(DeleteFeedbackRequest request)
        {
            var entity = new Feedback();
            entity.IdFeedback = request.IdFeedback;
            return entity;
        }

        public static Feedback ToEntity(EditFeedbackRequest request)
        {
            var entity = new Feedback();
            entity.IdFeedback = request.IdFeedback;
            entity.Titolo = request.Titolo;
            entity.Descrizione = request.Descrizione;
            return entity;
        }

        public static FeedbackDto ToDto(Feedback feedback)
        {
            var entity = new FeedbackDto();
            entity.IdFeedback = feedback.IdFeedback;
            entity.IdAcquirente = feedback.IdAcquirente;
            entity.Titolo = feedback.Titolo;
            entity.Descrizione = feedback.Descrizione;
            entity.Rating = feedback.Rating;
            return entity;
        }
    }
}