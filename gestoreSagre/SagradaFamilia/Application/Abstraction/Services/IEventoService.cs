using Application.Models.Request;
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
        Task<Evento> AddEventoAsync(AddEventoRequest request);
        Task<Evento> EditEventoAsync(EditEventoRequest request);
        Task DeleteEventoAsync(DeleteEventoRequest request);
    }
}
