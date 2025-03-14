using Application.Models.Dtos;
using Application.Models.Request;
using Models.Entities;

namespace Application.Mapper
{
    internal class AcquirenteMapper
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
            throw new NotImplementedException();
        }
    }
}