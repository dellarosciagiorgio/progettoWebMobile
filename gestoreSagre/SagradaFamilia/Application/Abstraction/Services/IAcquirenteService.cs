using Application.Models.Request;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstraction.Services
{
    public interface IAcquirenteService
    {
        Task<List<Acquirente>> GetAcquirentiAsync();

        Task<Acquirente> AddAcquirenteAsync(AddAcquirenteRequest request);
        Task<Acquirente> EditAcquirenteAsync(EditAcquirenteRequest request);

        //TODO ?
        //Task DeleteAcquirenteAsync(DeleteAcquirenteRequest request);
    }
}
