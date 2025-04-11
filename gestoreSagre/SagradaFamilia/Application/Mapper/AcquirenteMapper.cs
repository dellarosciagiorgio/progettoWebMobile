using Application.Models.Dtos;
using Application.Models.Requests;
using Microsoft.Extensions.DependencyInjection;
using Models.DetailedEntities;
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

            var user = new User();
            user.Email = request.Email;
            user.Password = request.Password;
            user.Ruolo = Ruolo.Acquirente;
            entity.User = user;

            return entity;
        }

        public static Acquirente ToEntity(EditAcquirenteRequest request)
        {
            var entity = new Acquirente();
            entity.Nome = request.Nome;
            entity.Id = (int)request.IdUser;
            entity.Cognome = request.Cognome;
            return entity;
        }

        public static AcquirenteDto ToDto(Acquirente acquirente)
        {
            AcquirenteDto acquirenteDto = new AcquirenteDto();
            acquirenteDto.IdAcquirente = acquirente.Id;
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