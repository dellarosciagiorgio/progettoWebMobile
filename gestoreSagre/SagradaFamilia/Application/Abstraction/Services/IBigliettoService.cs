using Application.Models.Requests;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstraction.Services
{
    public interface IBigliettoService
    {
        Task<List<Biglietto>> GetBigliettiByAcquirenteAsync(int idAcquirente);
        Task<List<Biglietto>> AddBigliettiAsync(AddBigliettoRequest request);
    }
}
