using Application.Models.Dtos;
using Application.Models.Request;
using Microsoft.Extensions.DependencyInjection;
using Models.Entities;

namespace Application.Mapper
{
    public class AcquirenteMapper
    {
        public static Acquirente ToEntity(AddAcquirenteRequest request)
        {
            var entity = new Acquirente();
            entity.Nome = request.Nome;
            entity.Cognome = request.Cognome;
            entity.Mail = request.Mail;
            return entity;
        }

        public static Acquirente ToEntity(EditAcquirenteRequest request)
        {
            var entity = new Acquirente();
            entity.Nome = request.Nome;
            entity.IdAcquirente = request.IdAcquirente;
            entity.Cognome = request.Cognome;
            entity.Mail = request.Mail;
            return entity;
        }

        public static AcquirenteDto ToDto(Acquirente acquirente)
        {
            AcquirenteDto acquirenteDto = new AcquirenteDto();
            acquirenteDto.IdAcquirente = acquirente.IdAcquirente;
            acquirenteDto.Cognome = acquirente.Cognome;
            acquirenteDto.Nome = acquirente.Nome;
            foreach (Feedback feedback in acquirente.Feedbacks)
            {
                acquirenteDto.IdFeedBacks.Add(feedback.IdFeedback);
            }
            return acquirenteDto;
        }
    }
}