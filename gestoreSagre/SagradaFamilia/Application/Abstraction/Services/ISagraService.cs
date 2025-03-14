using Application.Request;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstraction.Services
{
    public interface ISagraService
    {
        Task<List<Sagra>> GetSagreAsync();
        Task<Sagra> AddSagraAsync(AddSagraRequest request);
        Task<Sagra> EditSagraAsync(EditSagreRequest request);
        Task DeleteSagraAsync(DeleteSagraRequest request);
    }
}
