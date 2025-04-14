using Application.Models.Requests;
using Models.DetailedEntities;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstraction.Services
{
    public interface IEventoService
    {
        Task<List<Evento>> GetEventiAsync();
        Task<List<Evento>> GetEventiBySagraAsync(int idSagra);
        Task<List<Evento>> GetEventiAsync(int idUser, Ruolo ruolo, bool eventiFuturi);
        Task<Evento> AddEventoAsync(AddEventoRequest request);
        Task<Evento> EditEventoAsync(EditEventoRequest request);
        Task DeleteEventoAsync(DeleteEventoRequest request);
        Task<List<Evento>> GetEventiAsync(bool checkByFuture);
    }
}
