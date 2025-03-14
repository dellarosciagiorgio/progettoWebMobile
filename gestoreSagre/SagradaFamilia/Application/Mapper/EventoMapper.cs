using Application.Models.Dtos;
using Application.Models.Request;
using Models.Entities;

namespace Application.Mapper
{
    internal class EventoMapper
    {
        public static Evento ToEntity(AddEventoRequest request)
        {
            var entity = new Evento();
            entity.IdSagra = request.IdSagra;
            entity.InformazioniAggiuntive = request.InformazioniAggiuntive;
            entity.DataEvento = request.DataEvento;
            return entity;
        }

        public static Evento ToEntity(DeleteEventoRequest request)
        {
            var entity = new Evento();
            entity.IdEvento = request.IdEvento;
            return entity;
        }

        public static Evento ToEntity(EditEventoRequest request)
        {
            var entity = new Evento();
            entity.IdEvento = request.IdEvento;
            entity.InformazioniAggiuntive = request.InformazioniAggiuntive;
            entity.DataEvento = request.DataEvento;
            return entity;
        }
        public static EventoDto ToDto(Evento evento)
        {
            throw new NotImplementedException();
        }

    }
}