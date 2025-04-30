using Application.Models.Requests;
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
        Task<Sagra> EditSagraAsync(EditSagraRequest request);
        Task DeleteSagraAsync(DeleteSagraRequest request);
        Task<Sagra> GetSagreAsync(int id);
        Task<double> GetRatingSagraAsync(int id);
        Task<List<Sagra>> GetSagreByOrgAsync(int idOrg);
    }
}
