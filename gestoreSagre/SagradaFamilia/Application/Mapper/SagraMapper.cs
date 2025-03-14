using Application.Models.Dtos;
using Application.Models.Request;
using Models.Entities;

namespace Application.Mapper
{
    internal class SagraMapper
    {
        public static Sagra ToEntity(AddSagraRequest request)
        {
            var entity = new Sagra();
            entity.NomeSagra = request.NomeSagra;
            entity.Descrizione = request.Descrizione;
            return entity;
        }

        public static Sagra ToEntity(DeleteSagraRequest request)
        {
            var entity = new Sagra();
            entity.IdSagra = request.IdSagra;
            return entity;
        }

        public static Sagra ToEntity(EditSagraRequest request)
        {
            var entity = new Sagra();
            entity.IdSagra = request.IdSagra;
            entity.NomeSagra = request.NomeSagra;
            entity.Descrizione = request.Descrizione;
            return entity;
        }

        public static SagraDto ToDto(Sagra sagra)
        {
            throw new NotImplementedException();
        }
    }
}