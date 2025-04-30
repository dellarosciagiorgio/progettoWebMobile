using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstraction.Services
{
    public interface IUserService
    {
        Task<int> GetOrgIdByUserIdAsync(int? userId);
        Task<int> GetAcqIdByUserIdAsync(int userId);
    }
}
