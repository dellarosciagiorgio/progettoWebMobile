using Application.Models.Dtos;
using Application.Models.Requests;
using Models.Entities;

namespace Application.Mapper
{
    public class SagraMapper
    {
        public static Sagra ToEntity(AddSagraRequest request)
        {
            var entity = new Sagra();
            entity.IdOrganizzatore = (int)request.IdUser;
            entity.NomeSagra = request.NomeSagra;
            entity.Descrizione = request.Descrizione;
            return entity;
        }

        public static Sagra ToEntity(DeleteSagraRequest request)
        {
            var entity = new Sagra();
            entity.IdOrganizzatore = (int)request.IdUser;
            entity.IdSagra = request.IdSagra;
            return entity;
        }

        public static Sagra ToEntity(EditSagraRequest request)
        {
            var entity = new Sagra();
            entity.IdSagra = request.IdSagra;
            entity.IdOrganizzatore = (int)request.IdUser;
            entity.NomeSagra = request.NomeSagra;
            entity.Descrizione = request.Descrizione;
            return entity;
        }

        public static SagraDto ToDto(Sagra sagra)
        {
            SagraDto sagraDto = new SagraDto();
            sagraDto.IdSagra = sagra.IdSagra;
            sagraDto.NomeSagra = sagra.NomeSagra;
            sagraDto.IdOrganizzatore = sagra.IdOrganizzatore;
            sagraDto.Descrizione = sagra.Descrizione;
            foreach(Feedback feedback in sagra.Feedbacks)
            {
                sagraDto.IdFeedbacks.Add(feedback.IdFeedback);
            }
            foreach (Evento evento in sagra.Eventi)
            {
                sagraDto.IdEventi.Add(evento.IdEvento);
            }
            return sagraDto;
        }
    }
}